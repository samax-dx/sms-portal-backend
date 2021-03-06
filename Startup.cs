using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using sms_portal_backend.Helpers;
using SmsGateway;

namespace sms_portal_backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<smsportalContext>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "sms_portal_backend", Version = "v1" });
            });

            services.AddSingleton(Configuration.GetSection("JwtSettings").Get<JwtSettings>());
            services.AddScoped<JwtGenerator>();

            services.AddScoped(factory =>
            {
                return new SmsProviderSMPP();
            });
            services.AddScoped(factory =>
            {
                var endpoint = new EndPoint(new HttpConfig()
                {
                    BaseUrl = ConfigSGW.baseUrl,
                    ApiKey = ConfigSGW.apiKey,
                    ClientId = ConfigSGW.clientId
                });
                return new SmsProviderHttp(endpoint);
            });
            services.AddScoped(factory =>
            {
                var T = Type.GetType(Configuration.GetValue<string>("SmsProviderType"));
                return (ISmsProvider)factory.GetRequiredService(T);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "sms_portal_backend v1"));
            }

            app.UseCors(builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader();
            });

            app.UseRouting();

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
