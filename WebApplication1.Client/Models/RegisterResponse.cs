using WebApplication1.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Client.Models
{
    public class RegisterResponse
    {
        public Guid? UserId { get; set; }
        public string VerificationCode { get; set; }
    }
}
