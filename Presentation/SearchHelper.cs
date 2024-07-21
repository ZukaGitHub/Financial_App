using Presentation.Models.SearchEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SearchEngine
{
    public  static class SearchHelper
    {
        public static bool HasAnySearchCriteria(SearchEngineDTO searchEngine)
        {
            return !string.IsNullOrEmpty(searchEngine.SearchField) ||
                 
                   !string.IsNullOrEmpty(searchEngine.PersonalId) ||
                   searchEngine.SortOption != default ||
                   !string.IsNullOrEmpty(searchEngine.Id) ||
                   !searchEngine.PageNumber.HasValue ||
                   !searchEngine.PageSize.HasValue;
        }
    }
}
