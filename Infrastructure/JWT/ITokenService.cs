using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.JWT
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(User user);
    }
}
