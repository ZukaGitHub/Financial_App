using Domain.SharedModels;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SearchEngine.GetSearch
{
   
        public class GetSearchEngineCommandHandler : IRequestHandler<GetSearchEngineCommand, SearchEngineResponseModel>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetSearchEngineCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<SearchEngineResponseModel> Handle(GetSearchEngineCommand request, CancellationToken cancellationToken)
            {
                var searchEngine = await _unitOfWork.SearchEngineRepository.GetAsync(c=>c.Id==request.SearchId);

                if (searchEngine == null)
                {
                    return new SearchEngineResponseModel { Errors = new() { "Search engine not found." } };
                }

                return new SearchEngineResponseModel { SearchEngine = searchEngine };
            }
        }
    
}
