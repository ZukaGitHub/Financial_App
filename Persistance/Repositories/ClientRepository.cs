using Domain.Entities;
using Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class ClientRepository:BaseRepository<Client>,IClientRepository
    {
        public ClientRepository(FinancialAppDBContext context) : base(context) { }

    }
}
