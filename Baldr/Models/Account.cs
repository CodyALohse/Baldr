using Core;
using Baldr.Models.Enums;
using System.Collections.Generic;

namespace Baldr.Models
{
    public class Account : BaseModel
    {
        public virtual ICollection<Transaction> Transactions { get; set; }

        public string Name { get; set; }

        public string AccountNumber { get; set; }

        public decimal StartingBalance { get; set; }

        public decimal CurrentBalance { get; set; }

        public string Comment { get; set; }

        public decimal InterestRate { get; set; }

        public AccountType Type { get; set; }
    }
}
