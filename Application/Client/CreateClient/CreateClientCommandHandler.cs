using Domain;
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
        private readonly IUnitOfWork _unitOfWork;
        public CreateClientCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateClientResponseModel> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            try
            {             
                    await _unitOfWork.ClientRepository.AddAsync(request.Client,cancellationToken);
                    await _unitOfWork.ClientRepository.SaveChangesAsync();
                    return new CreateClientResponseModel() { IsCreated = true };
             
            }
            catch (Exception ex)
            {
                return new CreateClientResponseModel() { Errors = new List<string>() { ex.Message } };
            }
        }
    }
}
