using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalId { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public string MobileNumber { get; set; }
        public GenderType Gender { get; set; }
        public Address Address { get; set; }
        public List<Account> Accounts { get; set; }
    }
    public enum GenderType
    {
        Male,
        Female
    }

}
