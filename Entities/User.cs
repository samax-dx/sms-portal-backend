using System;

#nullable disable

namespace sms_portal_backend.Entities
{
    public partial class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}