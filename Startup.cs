using Microsoft.AspNetCore.DataProtection;

namespace GiveAID
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // In Startup.cs, add these lines to the ConfigureServices method
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(120);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });
            services.AddMvc();
        }
    }
}
