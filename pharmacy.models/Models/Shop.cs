using System;
using System.Collections.Generic;

namespace Pharmacy.Models
{
    public partial class Shop
    {
        public Shop()
        {
            CollectScript = new HashSet<CollectScript>();
            Customer = new HashSet<Customer>();
        }

        public Guid ShopId { get; set; }
        public string ShopName { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public Guid AddressId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public Address Address { get; set; }
        public ICollection<CollectScript> CollectScript { get; set; }
        public ICollection<Customer> Customer { get; set; }
    }
}
