using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Core.Entities
{
    public abstract class EntityBase : IEntityBase
    {
        public virtual Guid Id { get; set; } = new Guid();
        public virtual bool IsActive { get; set; } = true;
    }
}
