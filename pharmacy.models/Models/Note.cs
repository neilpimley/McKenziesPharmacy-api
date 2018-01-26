using System;
using System.Collections.Generic;

namespace Pharmacy.Models
{
    public partial class Note
    {
        public Note()
        {
            Order = new HashSet<Order>();
        }

        public Guid NoteId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedOn { get; set; }

        public ICollection<Order> Order { get; set; }
    }
}
