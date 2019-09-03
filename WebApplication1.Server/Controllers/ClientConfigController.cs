using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WebApplication1.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientConfigController : ControllerBase
    {
		private readonly IConfiguration _Configs;

		public ClientConfigController(IConfiguration pConfigSection)
		{
			_Configs = pConfigSection;
		}

		[HttpGet]
		public ConfigOptions Get()
		{
			// just return the APIService config..
			// TODO: create some leint config and return that instead
			var apiServicesSection = _Configs;
			var conf = new ConfigOptions();
			// bind config to object..
			apiServicesSection.Bind(conf);
			//_Configs.Bind(conf);

			return conf;
		}
	}
}