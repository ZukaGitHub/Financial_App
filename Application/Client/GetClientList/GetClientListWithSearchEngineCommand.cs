using Domain.SharedModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Client.GetClientList
{
    public class GetClientListWithSearchEngineCommand:IRequest<GetClientListWithSearchEngineResponseModel>
    {
        public Domain.Entities.SearchEngine SearchEngine { get; set; }
    }
}
