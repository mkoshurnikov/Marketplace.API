using Microsoft.AspNetCore.Identity;

namespace Marketplace.API.Configurations
{
    public static class IdentityConfiguration
    {
        public static IdentityBuilder AddIdentityConfiguration(this IServiceCollection services)
        {

            return services.AddIdentityCore<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            });
        }
    }
}
