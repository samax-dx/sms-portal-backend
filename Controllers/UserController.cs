using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using sms_portal_backend.Entities;
using sms_portal_backend.Models;
using sms_portal_backend.Helpers;


namespace sms_portal_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private DbContext db;
        private JwtGenerator jwtGenerator;

        public UserController(smsportalContext db, JwtGenerator jwtGenerator)
        {
            this.db = db;
            this.jwtGenerator = jwtGenerator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthenticateRequest model)
        {
            var user = await db.Set<User>().SingleOrDefaultAsync(x => x.Username == model.Username && x.Password == model.Password);

            if (user == null) return BadRequest(new { message = "Incorrect Username or password" });

            var token = jwtGenerator.generateToken(user);

            return Ok(new AuthenticateResponse(user, token));
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<User>> Get()
        {
            var userId = long.Parse(HttpContext.Items["user"].ToString());
            var user = await db.FindAsync<User>(userId);
            return user;
        }
    }
}
