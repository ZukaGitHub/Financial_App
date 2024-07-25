using Application.Client.GetClientList;
using Domain.SharedModels;
using Domain;
using MediatR;
using Domain.Entities;
using System.Linq.Expressions;

public class GetClientListWithSearchEngineCommandHandler : IRequestHandler<GetClientListWithSearchEngineCommand, GetClientListWithSearchEngineResponseModel>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetClientListWithSearchEngineCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetClientListWithSearchEngineResponseModel> Handle(GetClientListWithSearchEngineCommand command, CancellationToken cancellationToken = default)
    {
        var searchEngine = command.SearchEngine;

        // Retrieve existing search engine entry if searchId is provided
        if (!string.IsNullOrEmpty(searchEngine.Id))
        {
            var existingSearchEngine = await _unitOfWork.SearchEngineRepository.GetAsync(s=>s.Id==searchEngine.Id,null, cancellationToken);

            if (existingSearchEngine != null)
            {
                // Update only the provided values
                if (searchEngine.SortOption != default && searchEngine.SortOption != existingSearchEngine.SortOption)
                {
                    existingSearchEngine.SortOption = searchEngine.SortOption;
                }

                if (!string.IsNullOrEmpty(searchEngine.SearchField) && searchEngine.SearchField != existingSearchEngine.SearchField)
                {
                    existingSearchEngine.SearchField = searchEngine.SearchField;
                }

                if (!string.IsNullOrEmpty(searchEngine.PersonalId) && searchEngine.PersonalId != existingSearchEngine.PersonalId)
                {
                    existingSearchEngine.PersonalId = searchEngine.PersonalId;
                }

                if (searchEngine.PageNumber.HasValue && searchEngine.PageNumber.Value != existingSearchEngine.PageNumber)
                {
                    existingSearchEngine.PageNumber = searchEngine.PageNumber.Value;
                }

                if (searchEngine.PageSize.HasValue && searchEngine.PageSize.Value != existingSearchEngine.PageSize)
                {
                    existingSearchEngine.PageSize = searchEngine.PageSize.Value;
                }
               
                existingSearchEngine.SearchDate = DateTime.Now;
            
                await _unitOfWork.SearchEngineRepository.UpdateAsync(existingSearchEngine);
                await _unitOfWork.SearchEngineRepository.SaveChangesAsync(cancellationToken);

                searchEngine = existingSearchEngine;
            }
        }
        else
        {
            // If no searchId provided, create a new SearchEngine entry
            searchEngine.Id = Guid.NewGuid().ToString();
            searchEngine.PageNumber = searchEngine.PageNumber ?? 1;
            searchEngine.PageSize = searchEngine.PageSize ?? 9;
            searchEngine.SearchDate = DateTime.Now;

            await _unitOfWork.SearchEngineRepository.AddAsync(searchEngine);
            await _unitOfWork.SearchEngineRepository.SaveChangesAsync(cancellationToken);
        }
        var includeExpressions = new Expression<Func<Client, object>>[]
       {
        client => client.Accounts,
        client => client.Address
       };

        var clientsList = await _unitOfWork.ClientRepository.GetAllPaginatedAsync(
            client =>
                (string.IsNullOrEmpty(searchEngine.SearchField) ||
                client.Email.Contains(searchEngine.SearchField) ||
                client.FirstName.Contains(searchEngine.SearchField) ||
                client.LastName.Contains(searchEngine.SearchField))     
                &&
                (string.IsNullOrEmpty(searchEngine.PersonalId) || client.PersonalId == searchEngine.PersonalId),
            searchEngine.PageNumber.Value,
            searchEngine.PageSize.Value,
            includeExpressions,
            cancellationToken
        );
      
        if (searchEngine.SortOption != default)
        {
            clientsList.PagedItems = searchEngine.SortOption switch
            {
                SearchOptionENUM.EmailAsc => clientsList.PagedItems.OrderBy(c => c.Email).ToList(),
                SearchOptionENUM.EmailDesc => clientsList.PagedItems.OrderByDescending(c => c.Email).ToList(),
                SearchOptionENUM.FirstNameAsc => clientsList.PagedItems.OrderBy(c => c.FirstName).ToList(),
                SearchOptionENUM.FirstNameDesc => clientsList.PagedItems.OrderByDescending(c => c.FirstName).ToList(),
                SearchOptionENUM.LastNameAsc => clientsList.PagedItems.OrderBy(c => c.LastName).ToList(),
                SearchOptionENUM.LastNameDesc => clientsList.PagedItems.OrderByDescending(c => c.LastName).ToList(),
                _ => clientsList.PagedItems
            };
        }


      

        return new GetClientListWithSearchEngineResponseModel
        {
            Clients = clientsList.PagedItems,
            PageNumber = searchEngine.PageNumber,
            PageCount = clientsList.PageCount,
            SearchId=searchEngine.Id
        };
    }
}
