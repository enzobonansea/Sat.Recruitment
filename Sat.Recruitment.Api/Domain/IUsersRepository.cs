using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sat.Recruitment.Api.Domain
{
    public interface IUsersRepository
    {
        Task<List<User>> GetAll();
    }
}