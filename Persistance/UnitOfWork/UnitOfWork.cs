using Domain;
using Domain.IRepositories;
using Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FinancialAppDBContext _context;

        private IClientRepository _clientRepository;
        private IAccountRepository _accountRepository;
        private ISearchEngineRepository _searchEngineRepository;
        public UnitOfWork(FinancialAppDBContext context)
        {
            _context= context;
        }



        public IAccountRepository AccountRepository => _accountRepository ??= new AccountRepository(_context);
        public IClientRepository ClientRepository => _clientRepository ??= new ClientRepository(_context);
        public ISearchEngineRepository SearchEngineRepository => _searchEngineRepository ??= new SearchEngineRepository(_context);
        public Task<int> SaveAsync() => _context.SaveChangesAsync();
    }

}