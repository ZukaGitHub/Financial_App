using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public Client Client { get; set; }
        public string AccountNumber { get; set; }
        public int ClientId { get; set; }
        public decimal Balance { get; set; }
    }
}
