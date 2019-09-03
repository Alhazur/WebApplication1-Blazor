using WebApplication1.Client.Models;
using WebApplication1.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Client.Services
{
	public interface IAuthService
	{
		Task<ReturnValue> Logout();
		Task<ReturnValue<LoginResponse>> Login(LoginModel loginModel);
		Task<ReturnValue<RegisterResponse>> Register(RegisterModel registerModel);

        Task<ReturnValue> test();

    }
}
