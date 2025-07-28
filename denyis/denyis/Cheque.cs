using System;

namespace denyis
{
    public class Cheque
    {
        public string Number { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public Cheque()
        {
            Date = DateTime.Now;
        }

        public Cheque(string number, decimal amount, DateTime date, string description = "")
        {
            Number = number;
            Amount = amount;
            Date = date;
            Description = description;
        }
    }
} 