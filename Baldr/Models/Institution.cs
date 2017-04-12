using Core;
using System.Collections.Generic;

namespace Baldr.Models
{
    public class Institution : BaseModel
    {
        public virtual ICollection<Account> Accounts { get; set; }

        public string Name { get; set; }

        public Contact ContactInfo { get; set; }

        public bool IsActive { get; set; }
    }
}
