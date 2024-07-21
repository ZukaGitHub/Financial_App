using Domain.SharedModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SearchEngine.GetSearchSuggestions
{
    public class GetSearchEngineSuggestionsCommand : IRequest<SearchEngineListResponseModel>
    {
        public string UserId { get; set; }
    }
}
