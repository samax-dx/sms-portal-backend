using System;
using System.Collections.Generic;

#nullable disable

namespace sms_portal_backend.Entities
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
