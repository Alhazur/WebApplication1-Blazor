using System.Collections.Generic;

namespace WebApplication1.Client.Services
{
    public class RegisterResult
    {
        public bool Successful { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}