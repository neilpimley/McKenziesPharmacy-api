using System;
using System.Collections.Generic;

namespace Pharmacy.Models
{
    public partial class Favourite
    {
        public Guid FavouriteId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid DrugId { get; set; }
        public DateTime CreatedOn { get; set; }

        public Customer Customer { get; set; }
        public Drug Drug { get; set; }
    }
}
