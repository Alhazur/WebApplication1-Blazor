namespace WebApplication1.Shared
{
    public class ConfigOptions
    {

        public ConfigOptionsAPIServices APIServices { get; set; }
        public WebSettingsCls WebSettings { get; set; }

        public class WebSettingsCls
        {
            public string PaymentCheckoutKey { get; set; }
            public string PaymentCheckoutPage { get; set; }
        }
    }

    public class ConfigOptionsAPIServices
    {
        public ConfigOptionsBaseAPI PaymentAPI { get; set; }
        public ConfigOptionsBaseAPI UserServiceAPI { get; set; }
        public ConfigOptionsBaseAPI DataServiceAPI { get; set; }
        public ConfigOptionsBaseAPI UserAdminServiceAPI { get; set; }
    }

    public class ConfigOptionsBaseAPI
    {
        public string Url { get; set; }
        public string HealthCheckUrl { get; set; }
        public AuthCls Auth { get; set; }

        public class AuthCls
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
