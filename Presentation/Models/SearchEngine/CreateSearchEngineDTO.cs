using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Models.SearchEngine
{
    public class CreateSearchEngineDTO
    {
        public SearchOptionENUM SortOption { get; set; }
        public string? SearchField { get; set; }
        public string? PersonalId { get; set; }
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 9;
    }
}
