using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MiriProjectManager.Server.MiddleWares
{
    public class JwtMiddleware
    {
            private readonly RequestDelegate _next;
            private readonly string _secret;

            public JwtMiddleware(RequestDelegate next, IConfiguration config)
            {
                _next = next;
                _secret = config["JwtSecret"] ?? "superdupersecretkeythathastobeatleast265bitslong";
            }

            public async Task Invoke(HttpContext context)
            {
                var path = context.Request.Path.Value?.ToLower();

                // Skip auth for login and register
                if (path == "/api/auth/login" || path == "/api/auth/register" || path.StartsWith("/swagger"))
                {
                    await _next(context);
                    return;
                }

                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                if (token != null && ValidateToken(token))
                {
                    await _next(context); // valid
                    return;
                }

                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
            }

            private bool ValidateToken(string token)
            {
                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.UTF8.GetBytes(_secret);
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    return true;
                }
                catch
                {
                    return false;
                }
            }
    }
}
