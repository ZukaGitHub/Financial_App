using Domain;
using Domain.Entities;
using Domain.SharedModels;
using Infrastructure.FileManager;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Client.UpdateClient
{
    public class UpdateClientCommandHandler:IRequestHandler<UpdateClientCommand,UpdateClientResponseModel>
    {
        private IUnitOfWork _unitOfWork;
        private IFileManager _fileManager;

        public UpdateClientCommandHandler(IUnitOfWork unitOfWork,IFileManager fileManager)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
        }
        public async Task<UpdateClientResponseModel> Handle(UpdateClientCommand command,CancellationToken cancellationToken)
        {
            var includeExpressions = new Expression<Func<Domain.Entities.Client, object>>[]
                                  {
                                    client => client.Accounts,
                                    client => client.Address
                                  };
            var client = await _unitOfWork.ClientRepository.GetAsync(c => c.Id == command.UpdatedClient.Id,includeExpressions);
            var response=new UpdateClientResponseModel();

            if (client == null)
            {
                response.Errors=new List<string>() {"Client was not found"};
                return response;
            }
            client.Email = command.UpdatedClient.Email ?? client.Email;
            client.FirstName = command.UpdatedClient.FirstName ?? client.FirstName;
            client.LastName = command.UpdatedClient.LastName ?? client.LastName;
            client.MobileNumber = command.UpdatedClient.MobileNumber ?? client.MobileNumber;
            client.Gender = command.UpdatedClient.Gender != GenderType.Male && command.UpdatedClient.Gender != GenderType.Female
                ? client.Gender
                : command.UpdatedClient.Gender;

           
            if (command.UpdatedClient.Address != null)
            {
                client.Address = new Address
                {
                    Id = command.UpdatedClient.Address.Id,
                    City = command.UpdatedClient.Address.City ?? client.Address.City,
                    Country = command.UpdatedClient.Address.Country ?? client.Address.Country,
                    ZipCode = command.UpdatedClient.Address.ZipCode ?? client.Address.ZipCode,
                    Street = command.UpdatedClient.Address.Street ?? client.Address.Street,
               
                };
            }
            if (command.UpdatedClient.Accounts != null && command.UpdatedClient.Accounts.Count>0)
            {
                _unitOfWork.AccountRepository.RemoveRange(client.Accounts);
                client.Accounts = command.UpdatedClient.Accounts;
            }
                if (command.UpdatedClient.ProfilePhotoUrl != null)
            {
            
                if (!string.IsNullOrEmpty(client.ProfilePhotoUrl))
                {
                    await _fileManager.DeleteFileAsync(Path.GetFileName(client.ProfilePhotoUrl));
                }

             
                client.ProfilePhotoUrl = command.UpdatedClient.ProfilePhotoUrl;
            }
       
                await _unitOfWork.ClientRepository.UpdateAsync(client, cancellationToken);
                await _unitOfWork.ClientRepository.SaveChangesAsync();
                response.IsUpdated = true;                
            return response;
        }
    }
}
