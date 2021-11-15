using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Text.Json;
using SmsGateway;

namespace sms_portal_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SmsTaskController : ControllerBase
    {
        [HttpPost("SendSMS")]
        public async Task<string> SendSms([FromBody] SmsTask smsTask)
        {
            EndPoint endPoint = new EndPoint(new HttpConfig()
            {
                BaseUrl = ConfigSGW.baseUrl,
                UrlSuffix = "/SendSMS",
                ApiKey = ConfigSGW.apiKey,
                ClientId = ConfigSGW.clientId
            });

            ISmsProvider smsProvider = new SmsProviderHttp(endPoint);

            return await smsProvider.SendSms(smsTask);
        }
    }
}