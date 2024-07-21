using Domain;
using Domain.SharedModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SearchEngine.UpdateSearch
{
    public class UpdateSearchEngineCommandHandler : IRequestHandler<UpdateSearchEngineCommand,SearchEngineResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSearchEngineCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SearchEngineResponseModel> Handle(UpdateSearchEngineCommand command, CancellationToken cancellationToken)
        {
            var searchEngine = command.SearchEngine;

        
            var existingSearchEngine = await _unitOfWork.SearchEngineRepository.GetAsync(s=>s.Id==searchEngine.Id);

            if (existingSearchEngine == null)
            {
                return new SearchEngineResponseModel { Errors = new() { "Search query not found." } };
            }
         
            existingSearchEngine.SearchField = searchEngine.SearchField;
            existingSearchEngine.PersonalId = searchEngine.PersonalId;
            existingSearchEngine.SortOption = searchEngine.SortOption;
            existingSearchEngine.PageNumber = searchEngine.PageNumber;
            existingSearchEngine.PageSize = searchEngine.PageSize;
            existingSearchEngine.SearchDate = DateTime.Now;

          
            await _unitOfWork.SearchEngineRepository.UpdateAsync(existingSearchEngine);
            await _unitOfWork.SearchEngineRepository.SaveChangesAsync(cancellationToken);

            return new SearchEngineResponseModel { SearchEngine = existingSearchEngine };
        }
    }
}
