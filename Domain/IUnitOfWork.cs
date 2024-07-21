using Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IUnitOfWork
    {
        IClientRepository ClientRepository { get; }
        IAccountRepository AccountRepository { get; }
        Task<int> SaveAsync();
    }
}
