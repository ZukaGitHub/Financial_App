using Domain;
using Domain.SharedModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SearchEngine.GetSearchSuggestions
{
    public class GetSearchEngineSuggestionsCommandHandler : IRequestHandler<GetSearchEngineSuggestionsCommand, SearchEngineListResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSearchEngineSuggestionsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SearchEngineListResponseModel> Handle(GetSearchEngineSuggestionsCommand command, CancellationToken cancellationToken)
        {
            var userId = command.UserId;

            
            var searchEngines = await _unitOfWork.SearchEngineRepository
                .GetAllAsync(s=>s.UserId==command.UserId); 

            if (searchEngines == null || !searchEngines.Any())
            {
                return new SearchEngineListResponseModel { Errors = new() { "No search engines found." } };
            }

            return new SearchEngineListResponseModel { SearchQueries = searchEngines.Take(3).ToList() };
        }
    }
}
