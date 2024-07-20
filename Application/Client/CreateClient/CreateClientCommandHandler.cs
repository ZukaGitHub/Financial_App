using Domain.SharedModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Client.CreateClient
{
    public class CreateClientCommandHandler :IRequestHandler<CreateClientCommand,CreateClientResponseModel>
    {
        public CreateClientCommandHandler()
        {

        }

        public Task<CreateClientResponseModel> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
