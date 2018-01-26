using System;
using System.Collections.Generic;

namespace Pharmacy.Models
{
    public partial class Address
    {
        public Address()
        {
            Customer = new HashSet<Customer>();
            Practice = new HashSet<Practice>();
            Shop = new HashSet<Shop>();
        }

        public Guid AddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public DateTime CreatedOn { get; set; }

        public ICollection<Customer> Customer { get; set; }
        public ICollection<Practice> Practice { get; set; }
        public ICollection<Shop> Shop { get; set; }
    }
}
