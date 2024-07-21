using Application.Client.GetClientList;
using Domain.SharedModels;
using Domain;
using MediatR;
using Domain.Entities;

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
            var existingSearchEngine = await _unitOfWork.SearchEngineRepository.GetAsync(s=>s.Id==searchEngine.Id, cancellationToken);

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
            cancellationToken
        );
        if (searchEngine.SortOption != default)
        {
            clientsList = searchEngine.SortOption switch
            {
                SearchOptionENUM.EmailAsc => clientsList.OrderBy(c => c.Email).ToList(),
                SearchOptionENUM.EmailDesc => clientsList.OrderByDescending(c => c.Email).ToList(),
                SearchOptionENUM.FirstNameAsc => clientsList.OrderBy(c => c.FirstName).ToList(),
                SearchOptionENUM.FirstNameDesc => clientsList.OrderByDescending(c => c.FirstName).ToList(),
                SearchOptionENUM.LastNameAsc => clientsList.OrderBy(c => c.LastName).ToList(),
                SearchOptionENUM.LastNameDesc => clientsList.OrderByDescending(c => c.LastName).ToList(),
                _ => clientsList
            };
        }


        var pageCount = clientsList.Count() > 0 ? (int)Math.Ceiling((double)clientsList.Count() / searchEngine.PageSize.Value) : 0;

        return new GetClientListWithSearchEngineResponseModel
        {
            Clients = clientsList,
            PageNumber = searchEngine.PageNumber,
            PageCount = pageCount
        };
    }
}
