using System;

namespace Domains.Entities
{
    public abstract class AbstractEntity : BaseEntity<Guid>
    {
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}