using System;
using JWT.Builder;
using JWT.Algorithms;

using sms_portal_backend.Entities;

namespace sms_portal_backend.Helpers
{
    public class JwtGenerator
    {
        private readonly JwtSettings jwtSettings;

        public JwtGenerator(JwtSettings jwtSettings)
        {
            this.jwtSettings = jwtSettings;
        }

        public string generateToken(User user)
        {
            return JwtBuilder.Create()
                    .WithAlgorithm(new HMACSHA256Algorithm()) // symmetric
                    .WithSecret(jwtSettings.Secret)
                    .AddClaim("exp", DateTimeOffset.UtcNow.AddDays(7).ToUnixTimeSeconds())
                    .AddClaim("id", user.Id.ToString())
                    .Encode();
        }
    }
}
