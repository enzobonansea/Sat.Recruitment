using System;
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

        public async Task Execute(User user) 
        {
            if (await this.usersRepository.Exists(user)) 
            {
                Debug.WriteLine("The user is duplicated");
                throw new UserDuplicatedException();
            }
            
            await this.usersRepository.Save(user);
            Debug.WriteLine("User Created");
        }
    }
}