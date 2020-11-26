using System;
using System.Collections.Generic;

namespace Utilities.Domain.Customer.Entities
{
    public partial class NoteEntity : Core.Framework.IAuditable
    {
        public long NoteId { get; set; }
        public long CustomerId { get; set; }
        public int NoteTypeId { get; set; }
        public string Note1 { get; set; }
        public bool IsSuppressed { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual CustomerEntity Customer { get; set; }
        public virtual NoteTypeEntity NoteType { get; set; }
    }
}
