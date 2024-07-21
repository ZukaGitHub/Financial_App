using Domain.SharedModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SearchEngine.GetSearch
{
    public class GetSearchEngineCommand: IRequest<SearchEngineResponseModel>
    {
        public string SearchId { get; set; }
    }
}
