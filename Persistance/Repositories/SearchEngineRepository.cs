using Domain.Entities;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.IRepositories;

namespace Persistance.Repositories
{
    public class SearchEngineRepository : BaseRepository<SearchEngine>, ISearchEngineRepository
    {
        public SearchEngineRepository(FinancialAppDBContext context) : base(context) { }
    }
}
