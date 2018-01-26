using System;
using System.Collections.Generic;

namespace Pharmacy.Models
{
    public partial class Title
    {
        public Title()
        {
            Customer = new HashSet<Customer>();
            Doctor = new HashSet<Doctor>();
        }

        public Guid TitleId { get; set; }
        public string TitleName { get; set; }
        public DateTime CreatedOn { get; set; }

        public ICollection<Customer> Customer { get; set; }
        public ICollection<Doctor> Doctor { get; set; }
    }
}
