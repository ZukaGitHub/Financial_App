using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Models.Client
{
    public class UpdateClientDTO
    {
        [Required]
        public int Id { get; set; }      
        [EmailAddress]
        public string Email { get; set; }      
        [StringLength(60, ErrorMessage = "First name cannot exceed 60 characters.")]
        public string FirstName { get; set; }       
        [StringLength(60, ErrorMessage = "Last name cannot exceed 60 characters.")]
        public string LastName { get; set; }              
        [Phone]
        public string MobileNumber { get; set; }       
        public GenderType Gender { get; set; }       
        public AddressDTOForCreate Address { get; set; }
    }
}
