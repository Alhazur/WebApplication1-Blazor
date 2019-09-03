using WebApplication1.Shared;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace WebApplication1.Client.Services
{
    public class B75Config
	{
		private readonly HttpClient _HttpClient;
		
		// set up some standard options that can be used
		public readonly JsonSerializerOptions DefaultJsonSerializerOptions = new JsonSerializerOptions() {
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
				IgnoreNullValues = true			
			};

		// property to check to see if the config has been loaded or not
		public bool ConfigLoaded = false;

		//ConfigOptionsAPIServices _ConfigOptionsAPIServices;
		public ConfigOptionsAPIServices ConfigOptionsAPIServices { get => _ConfigOptions.APIServices; }

        ConfigOptions _ConfigOptions;
        public ConfigOptions ConfigOptions { get => _ConfigOptions; }

        public B75Config(HttpClient client)
		{
			_HttpClient = client;
		}

		/// <summary>
		/// Load config from backend
		/// </summary>
		/// <returns></returns>
		public async Task<ConfigOptions> GetConfig()
		{
            ConfigOptions conf = null;
			try
			{
				Console.WriteLine("B75Config");

				// just call the backend controller to get the config.
				conf = await _HttpClient.GetJsonAsync<ConfigOptions>("clientconfig");

                _ConfigOptions = conf;
				this.ConfigLoaded = true;
			}
			catch (Exception ex)
			{
				this.ConfigLoaded = false;
				Console.WriteLine(ex.ToString());
			}

			return conf;
		}
	}
}
