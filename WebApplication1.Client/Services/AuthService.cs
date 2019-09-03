using Blazored.LocalStorage;
using WebApplication1.Client.Models;
using WebApplication1.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json;

namespace WebApplication1.Client.Services
{
	// taken from here:
	// https://chrissainty.com/securing-your-blazor-apps-authentication-with-clientside-blazor-using-webapi-aspnet-core-identity/
	// .. and adjusted a bit
	public class AuthService : IAuthService
	{
		private readonly HttpClient _httpClient;
		private readonly AuthenticationStateProvider _authenticationStateProvider;
		//private readonly ILocalStorageService _localStorage;
		private readonly B75Config _B75Config;

		public AuthService(HttpClient httpClient,
						   AuthenticationStateProvider authenticationStateProvider,
						   //ILocalStorageService localStorage,
						   B75Config pB75Config
						   )
		{
			_httpClient = httpClient;
			_authenticationStateProvider = authenticationStateProvider;
			//_localStorage = localStorage;
			_B75Config = pB75Config;
		}

		//public async Task<RegisterResult> Register(RegisterModel registerModel)
		//{
		//	var result = await _httpClient.PostJsonAsync<RegisterResult>("api/accounts", registerModel);

		//	return result;
		//}

		public async Task<ReturnValue<LoginResponse>> Login(LoginModel loginModel)
		{
			// get config if it's not loaded already
			if (!_B75Config.ConfigLoaded)
				await _B75Config.GetConfig();

			ReturnValue<LoginResponse> rv = new ReturnValue<LoginResponse>();

			try
			{
				// create request to the user service authentication thing
				var request = new HttpRequestMessage() {
					Method = HttpMethod.Post,
					RequestUri = new Uri(System.IO.Path.Combine(_B75Config.ConfigOptionsAPIServices.UserServiceAPI.Url + "authenticate")),
					Content = new StringContent(JsonSerializer.Serialize(loginModel), System.Text.Encoding.UTF8, "application/json")
				};
				// get response..
				var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
				// .. and read content as stream, converting the json to real object
				using (var contentStream = await response.Content.ReadAsStreamAsync())
				{
					rv = await JsonSerializer.DeserializeAsync<ReturnValue<LoginResponse>>(contentStream, _B75Config.DefaultJsonSerializerOptions);
				}
				
				LoginResponse result = rv.ReturnObject;
				if (!rv.Error)
				{
					// if all ok, mark the user as logged in
					await ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(result.Token);
					// set the jwt token as default bearer for calls.. maybe not the best in all calls.. but it was in the example :)
					_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);

					return rv;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				rv.ErrorType = ReturnValue.ErrorTypes.Error;
				rv.ErrorException = ex;
				rv.Message = ex.ToString();
			}

			return rv;
		}

		public async Task<ReturnValue> Logout()
		{
			// get config if it's not loaded already
			if (!_B75Config.ConfigLoaded)
				await _B75Config.GetConfig();

			ReturnValue rv = new ReturnValue();

			try
			{
				// create request to user service logout thing
				var request = new HttpRequestMessage()
				{
					Method = HttpMethod.Post,
					RequestUri = new Uri(System.IO.Path.Combine(_B75Config.ConfigOptionsAPIServices.UserServiceAPI.Url + "logout"))
				};
				// call api and get response..
				var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
				using (var contentStream = await response.Content.ReadAsStreamAsync())
				{
					rv = await JsonSerializer.DeserializeAsync<ReturnValue>(contentStream, _B75Config.DefaultJsonSerializerOptions);
				}

				if (!rv.Error)
				{
					// if all ok, mark user as logged out
					await ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
					// .. remove default auth header.
					_httpClient.DefaultRequestHeaders.Authorization = null;

					return rv;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				rv.ErrorType = ReturnValue.ErrorTypes.Error;
				rv.ErrorException = ex;
				rv.Message = ex.ToString();
			}

			return rv;
			
		}

        public async Task<ReturnValue> test()
        {
            Console.WriteLine("regidrt");
            // get config if it's not loaded already
            if (!_B75Config.ConfigLoaded)
                await _B75Config.GetConfig();

            try {
                var paymentRequest = new {
                  UserId = "00000000-0000-0000-0000-000000000001",
                  Email = "string",
                  CheckoutUrl = "string",
                  ChargeNow = false
                };

                var request = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(System.IO.Path.Combine(_B75Config.ConfigOptionsAPIServices.PaymentAPI.Url, "test")),
                    Content = new StringContent(JsonSerializer.Serialize(paymentRequest), System.Text.Encoding.UTF8, "application/json")
                };

                string basicAuthString = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{_B75Config.ConfigOptionsAPIServices.PaymentAPI.Auth.Username}:{_B75Config.ConfigOptionsAPIServices.PaymentAPI.Auth.Password}"));
                //request.Headers.Authorization = new AuthenticationHeaderValue("Basic", basicAuthString);

                // get response..
                var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                // .. and read content as stream, converting the json to real object
                string resp = await response.Content.ReadAsStringAsync();

                //using (var contentStream = await response.Content.ReadAsStreamAsync())
                //{
                //    rv = await JsonSerializer.DeserializeAsync<ReturnValue<RegisterResponse>>(contentStream, _B75Config.DefaultJsonSerializerOptions);
                //}

                Console.WriteLine(resp);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
               
            }

            return null;
        }

        public async Task<ReturnValue<RegisterResponse>> Register(RegisterModel registerModel)
        {
            Console.WriteLine("regidrt");
            // get config if it's not loaded already
            if (!_B75Config.ConfigLoaded)
                await _B75Config.GetConfig();

            ReturnValue<RegisterResponse> rv = new ReturnValue<RegisterResponse>();

            try
            {
                var regReq = new
                {
                    Email = registerModel.Email,
                    Password = registerModel.Password,
                    DateOfBirth = new DateTime(1976, 8, 4),
                    MobilePhoneCountryCode = 0, //Input.CountryCode,
                    MobilePhoneNr = "", //mobileNrStr,
                    FirstName = "", //firstName,
                    LastName = "", //lastName
                    ExternalAuthenticator = "", //Input.ExternalAuthenticator,
                    ExternalAuthenticatorData = "" //(externalData != null ? Newtonsoft.Json.JsonConvert.SerializeObject(externalData) : null)
                };

                // create request to the user service authentication thing
                var request = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(System.IO.Path.Combine(_B75Config.ConfigOptionsAPIServices.UserServiceAPI.Url + "register")),
                    Content = new StringContent(JsonSerializer.Serialize(regReq), System.Text.Encoding.UTF8, "application/json")
                };
                // get response..
                var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                // .. and read content as stream, converting the json to real object
                using (var contentStream = await response.Content.ReadAsStreamAsync())
                {
                    rv = await JsonSerializer.DeserializeAsync<ReturnValue<RegisterResponse>>(contentStream, _B75Config.DefaultJsonSerializerOptions);
                }

                RegisterResponse result = rv.ReturnObject;
                if (!rv.Error)
                {

                    request = new HttpRequestMessage()
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri(System.IO.Path.Combine(_B75Config.ConfigOptionsAPIServices.UserServiceAPI.Url + "register")),
                        Content = new StringContent(JsonSerializer.Serialize(regReq), System.Text.Encoding.UTF8, "application/json")
                    };
                    
                    string basicAuthString = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{_B75Config.ConfigOptionsAPIServices.PaymentAPI.Auth.Username}:{_B75Config.ConfigOptionsAPIServices.PaymentAPI.Auth.Password}"));

                    request.Headers.Authorization = new AuthenticationHeaderValue("Basic", basicAuthString);
                    Console.WriteLine(JsonSerializer.Serialize(rv));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                rv.ErrorType = ReturnValue.ErrorTypes.Error;
                rv.ErrorException = ex;
                rv.Message = ex.ToString();
            }

            return rv;
        }
    }
}
