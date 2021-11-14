using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Text.Json;

using sms_portal_backend.Entities;
using sms_portal_backend.Models;


namespace sms_portal_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SmsTaskController : ControllerBase
    {
        private DbContext db;

        public SmsTaskController(smsportalContext db)
        {
            this.db = db;
        }

        [HttpPost]
        public async Task<IActionResult> SendSms([FromBody] SmsTask smsTask)
        {
            return await Task.Factory.StartNew<IActionResult>(() => Content(JsonSerializer.Serialize(smsTask)));
        }
    }
}