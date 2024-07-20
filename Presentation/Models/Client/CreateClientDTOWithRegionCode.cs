using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Models.Client
{
    public class CreateClientDTOWithRegionCode
    {
        [Required]
        public string RegionCode { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public CreateClientDTO CreateClientDTO { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "At least one account is required.")]
        public List<AccountDTOForCreate> AccountDTOForCreate { get; set; }
    }
}
