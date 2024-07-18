using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public class FinancialAppDBContext:IdentityDbContext<User>
    {
        public FinancialAppDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
    }
}
