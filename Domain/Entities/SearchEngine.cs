using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{  
        public class SearchEngine
        {
            public string Id { get; set; }
            public SearchOptionENUM SortOption { get; set; }
            public string? UserId { get; set; }
            public string? SearchField { get; set; }
            public string? PersonalId { get; set; }          
            public int? PageNumber { get; set; }
            public int? PageSize { get; set; }
            public DateTime SearchDate { get; set; } = DateTime.Now;
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
