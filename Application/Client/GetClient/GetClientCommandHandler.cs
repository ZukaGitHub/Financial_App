using Domain;
using Domain.SharedModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Client.GetClient
{
    public class GetClientCommandHandler:IRequestHandler<GetClientCommand,GetClientReponseModel>
    {
        private IUnitOfWork _unitOfWork;
        public GetClientCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<GetClientReponseModel> Handle(GetClientCommand command,CancellationToken cancellationToken=default)
        {
          
                var client = await _unitOfWork.ClientRepository.GetAsync(c => c.Id == command.Id);
                var responseModel = new GetClientReponseModel();
                if(client == null)
                {
                    responseModel.Errors = new List<string>() { "could not find client" };
                    return responseModel;
                }
                responseModel.Client = client;
                return responseModel;
            
          
        }
    }
}
