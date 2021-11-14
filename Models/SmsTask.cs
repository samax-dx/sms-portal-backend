using System.Collections.Generic;

namespace sms_portal_backend.Models
{
    public class SmsTask
    {
        public string CampaignName { get; set; }
        public string SenderId { get; set; }
        public List<string> Contacts { get; set; } = new List<string>();
        public string SmsBody { get; set; }
    }
}
