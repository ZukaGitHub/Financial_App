using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Models.SearchEngine
{
    public class UpdateSearchEngineDTO
    {
        [Required]
        public string? Id { get; set; }
        public SearchOptionENUM SortOption { get; set; }
        //Left for Testing Purposes
        [Required]
        public int? UserId { get; set; }
        public string? SearchField { get; set; }
        public string? PersonalId { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
