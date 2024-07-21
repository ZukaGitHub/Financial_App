using Domain;
using Domain.SharedModels;
using Infrastructure.FileManager;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Client.DeleteClient
{
    public class DeleteClientCommandHandler :IRequestHandler<DeleteClientCommand,DeleteClientResponseModel>
    {
        private IUnitOfWork _unitOfWork;
        private IFileManager _fileManager;
        public DeleteClientCommandHandler(IUnitOfWork unitOfWork,IFileManager fileManager)
        {
             _fileManager = fileManager;
             _unitOfWork = unitOfWork;
        }
        public async Task<DeleteClientResponseModel> Handle(DeleteClientCommand command,CancellationToken cancellationToken)
        {
            var client=await _unitOfWork.ClientRepository.GetAsync(c=>c.Id==command.Id);
            var responseModel=new DeleteClientResponseModel();
            if (client == null)
            {
                responseModel.Errors = new List<string>() {"Client Was Not Found"};
                return responseModel;
            }
            await _fileManager.DeleteFileAsync(client.ProfilePhotoUrl);

             _unitOfWork.ClientRepository.Remove(client);
            await _unitOfWork.ClientRepository.SaveChangesAsync();
            responseModel.IsDeleted = true;
            return responseModel;
        }
    }
}
