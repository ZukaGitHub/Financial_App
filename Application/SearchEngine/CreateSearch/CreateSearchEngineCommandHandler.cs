using Domain;
using Domain.SharedModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SearchEngine.CreateSearch
{
    public class CreateSearchEngineCommandHandler : IRequestHandler<CreateSearchEngineCommand, SearchEngineResponseModel>
    {
        private IUnitOfWork _unitOfWork;
        public CreateSearchEngineCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<SearchEngineResponseModel> Handle(CreateSearchEngineCommand command,CancellationToken cancellation=default)
        {
            var requestModel = command.SearchEngine;

            requestModel.Id = Guid.NewGuid().ToString();

            await _unitOfWork.SearchEngineRepository.AddAsync(requestModel);
            await _unitOfWork.SearchEngineRepository.SaveChangesAsync(cancellation);
            
            var response=new SearchEngineResponseModel();
            response.SearchEngine = requestModel;

            return response;
        }
    }
}
