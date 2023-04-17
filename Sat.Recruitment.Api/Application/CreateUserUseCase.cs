using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Sat.Recruitment.Api.Domain;

namespace Sat.Recruitment.Api.Application
{
    public class CreateUserUseCase
    {
        private readonly IUsersRepository usersRepository;

        public CreateUserUseCase(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task ExecuteAsync(User user) 
        {
            if (await this.usersRepository.ExistsAsync(user)) 
            {
                Debug.WriteLine("The user is duplicated");
                throw new UserDuplicatedException();
            }
            
            await this.usersRepository.SaveAsync(user);
            Debug.WriteLine("User Created");
        }
    }
}