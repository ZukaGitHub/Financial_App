using Domain.SharedModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Client.UpdateClient
{
    public class UpdateClientCommand:IRequest<UpdateClientResponseModel>
    {
        public Domain.Entities.Client UpdatedClient { get; set; }
    }
}
