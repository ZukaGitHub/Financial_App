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

        public UnitOfWork(FinancialAppDBContext context)
        {
            _context= context;
        }




        public IClientRepository ClientRepository => _clientRepository ??= new ClientRepository(_context);
        public Task<int> SaveAsync() => _context.SaveChangesAsync();
    }

}