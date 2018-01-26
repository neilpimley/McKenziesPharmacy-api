using System;
using System.Collections.Generic;

namespace Pharmacy.Models
{
    public partial class Drug
    {
        public Drug()
        {
            Favourite = new HashSet<Favourite>();
            OrderLine = new HashSet<OrderLine>();
        }

        public Guid DrugId { get; set; }
        public string DrugName { get; set; }
        public string DrugDose { get; set; }
        public int PackSize { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public ICollection<Favourite> Favourite { get; set; }
        public ICollection<OrderLine> OrderLine { get; set; }
    }
}
