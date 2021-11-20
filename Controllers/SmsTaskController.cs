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
        private readonly ISmsProvider smsProvider;

        public SmsTaskController(ISmsProvider smsProvider)
        {
            this.smsProvider = smsProvider;
        }

        [HttpPost("SendSMS")]
        public async Task<string> SendSms([FromBody] SmsTask smsTask)
        {
            return await smsProvider.SendSms(smsTask);
        }
    }
}