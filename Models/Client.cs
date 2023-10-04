using System;

namespace Models
{
    public class Client
    {
        public string Name { get; set; }
        public long UNP { get; set; }
        public DateOnly Date { get; set; }
        public decimal Sum { get; set; }
        public string Status { get; set; }
    }
}
