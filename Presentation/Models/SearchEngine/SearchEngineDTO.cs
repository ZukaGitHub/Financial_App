using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Models.SearchEngine
{
    public class SearchEngineDTO
    {
        public string? Id { get; set; }
        public SearchOptionENUM SortOption { get; set; }
        public int? UserId { get; set; }
        public string? SearchField { get; set; }
        public string? PersonalId { get; set; }    
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }

    public enum SearchOptionENUM
    {
        EmailAsc,
        EmailDesc,
        FirstNameAsc,
        FirstNameDesc,
        LastNameAsc,
        LastNameDesc,

    }
}
