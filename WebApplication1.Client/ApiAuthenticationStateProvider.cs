using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication1.Client.Models;
using WebApplication1.Shared;
using WebApplication1.Client.Services;

namespace WebApplication1.Client
{
	// taken from here:
	// https://chrissainty.com/securing-your-blazor-apps-authentication-with-clientside-blazor-using-webapi-aspnet-core-identity/
	// .. and adjusted a bit
	public class ApiAuthenticationStateProvider : AuthenticationStateProvider
	{
		private readonly HttpClient _httpClient;
		private readonly ILocalStorageService _localStorage;
		private readonly B75Config _B75Config;

		public ApiAuthenticationStateProvider(HttpClient httpClient, 
			ILocalStorageService localStorage,
			B75Config pB75Config)
		{
			_httpClient = httpClient;
			_localStorage = localStorage;
			_B75Config = pB75Config;
		}
		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			var savedToken = await _localStorage.GetItemAsync<string>("authToken");

			if (string.IsNullOrWhiteSpace(savedToken))
			{
				return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
			}

			if (!_B75Config.ConfigLoaded)
				await _B75Config.GetConfig();

			var identity = new ClaimsIdentity();
			try
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", savedToken);
				//var userInfo = await _httpClient.GetJsonAsync<ReturnValue<Buster75User>>("api/accounts/user");
				ReturnValue<GetUserResponse> rvCall = await _httpClient.GetJsonAsync<ReturnValue<GetUserResponse>>(_B75Config.ConfigOptionsAPIServices.UserServiceAPI.Url + "currentuser");
				if (!rvCall.Error && rvCall.ReturnObject?.UserInfo != null)
					identity = new ClaimsIdentity(ParseClaimsFromJwt(savedToken), "jwt");
				else
					await MarkUserAsLoggedOut();
			}
			catch(Exception ex)
			{
				Console.WriteLine("GetAuthenticationStateAsync - currentuser. " + ex.Message);
				await MarkUserAsLoggedOut();
			}

			return new AuthenticationState(new ClaimsPrincipal(identity));
		}

		public async Task MarkUserAsAuthenticated(string token)
		{
			var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt"));
			var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
			await _localStorage.SetItemAsync("authToken", token);
			NotifyAuthenticationStateChanged(authState);
		}

		public async Task MarkUserAsLoggedOut()
		{
			var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
			var authState = Task.FromResult(new AuthenticationState(anonymousUser));
			await _localStorage.RemoveItemAsync("authToken");
			NotifyAuthenticationStateChanged(authState);
		}

		private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
		{
			var claims = new List<Claim>();
			var payload = jwt.Split('.')[1];
			var jsonBytes = ParseBase64WithoutPadding(payload);
			var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

			keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

			if (roles != null)
			{
				if (roles.ToString().Trim().StartsWith("["))
				{
					var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

					foreach (var parsedRole in parsedRoles)
					{
						claims.Add(new Claim(ClaimTypes.Role, parsedRole));
					}
				}
				else
				{
					claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
				}

				keyValuePairs.Remove(ClaimTypes.Role);
			}

			claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

			return claims;
		}

		private byte[] ParseBase64WithoutPadding(string base64)
		{
			switch (base64.Length % 4)
			{
				case 2: base64 += "=="; break;
				case 3: base64 += "="; break;
			}
			return Convert.FromBase64String(base64);
		}
	}
}
