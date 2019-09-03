using WebApplication1.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Client.Models
{
	public class LoginResponse
	{
		public Buster75User UserInfo { get; set; }
		public string Token { get; set; }
		public string RefreshToken { get; set; }
	}
}
