using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Models.Client
{
    public class UpdateClientWithImageAndRegionCode
    {
        public string RegionCode { get; set; }
        public UpdateClientDTO UpdateClientDTO { get; set; }
        public IFormFile  Image { get; set; }
        public List<AccountDTO> AccountsDTO { get; set; }

        [StringLength(11, MinimumLength = 11, ErrorMessage = "Personal ID must be exactly 11 characters.")]
        public string PersonalId { get; set; }
    }
}
