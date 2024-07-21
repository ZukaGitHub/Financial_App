using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Models.Client
{
    public class CreateClientDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "First name cannot exceed 60 characters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "Last name cannot exceed 60 characters.")]
        public string LastName { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Personal ID must be exactly 11 characters.")]
        public string PersonalId { get; set; }

        [Required]
        [Phone]
        public string MobileNumber { get; set; }

        [Required]
        public GenderType Gender { get; set; }

        [Required]
        public AddressDTOForCreate Address { get; set; }


    }
}
