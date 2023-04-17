using System.Threading.Tasks;
using System.Collections.Generic;

using Sat.Recruitment.Api.Domain;

namespace Sat.Recruitment.Api.Application
{
    public interface IUsersRepository
    {
        Task<bool> ExistsAsync(User aUser);
        Task SaveAsync(User aUser);
    }
}