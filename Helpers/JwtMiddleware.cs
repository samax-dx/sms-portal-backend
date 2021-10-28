using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using JWT.Builder;
using JWT.Algorithms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms_portal_backend.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, JwtSettings jwtSettings)
        {
            _next = next;
            _appSettings = jwtSettings;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null) attachUserToContext(context, token);

            await _next(context);
        }

        private void attachUserToContext(HttpContext context, string token)
        {
            var credential = JwtBuilder.Create()
                     .WithAlgorithm(new HMACSHA256Algorithm()) // symmetric
                     .WithSecret(_appSettings.Secret)
                     .MustVerifySignature()
                     .Decode<IDictionary<string, object>>(token);

            try
            {
                var userId = long.Parse(credential["id"].ToString());
                context.Items["user"] = userId;
            } catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}