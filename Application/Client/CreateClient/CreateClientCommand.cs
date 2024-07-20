using Domain.SharedModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Client.CreateClient
{
    public class CreateClientCommand : IRequest<CreateClientResponseModel>
    {
        public Domain.Entities.Client Client { get; set; }
    }
}
