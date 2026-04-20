using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api.DependencyInjections
{
    public static class AuthentificationDependencyInjectionExtensions
    {
        public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),
                            ValidateIssuer = true,
                            ValidIssuer = configuration["Jwt:Issuer"],
                            ValidateAudience = true,
                            ValidAudience = configuration["Jwt:Audience"]
                        };
                    });

            services.AddAuthorization();
        }
    }
}
