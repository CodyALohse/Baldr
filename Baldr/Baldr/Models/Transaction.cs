using Core;
using Baldr.Models.Enums;
using System;

namespace Baldr.Models
{
    public class Transaction : BaseModel
    {
        public DateTimeOffset Date { get; set; }

        public decimal Amount { get; set; }

        public decimal AlternateAmount { get; set; }

        public bool IsComplete { get; set; }

        public string Comment { get; set; }

        public TransactionType Type { get; set; }
    }
}
