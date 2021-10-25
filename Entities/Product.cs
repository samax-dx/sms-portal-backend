using System;
using System.Collections.Generic;

#nullable disable

namespace sms_portal_backend.Entities
{
    public partial class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public long? CategoryId { get; set; }

        public virtual Category CategoryNavigation { get; set; }
    }
}
