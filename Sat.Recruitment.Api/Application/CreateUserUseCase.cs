using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Sat.Recruitment.Api.Domain;

namespace Sat.Recruitment.Api.Application
{
    public class CreateUserUseCase
    {
        private readonly IUsersRepository usersRepository;
        private readonly ILogger<CreateUserUseCase> logger;

        public CreateUserUseCase(IUsersRepository usersRepository, ILogger<CreateUserUseCase> logger)
        {
            this.usersRepository = usersRepository;
            this.logger = logger;
        }

        public async Task ExecuteAsync(User user) 
        {
            if (await this.usersRepository.ExistsAsync(user)) 
            {
                this.logger.LogInformation("The user is duplicated");
                throw new UserDuplicatedException();
            }
            
            await this.usersRepository.SaveAsync(user);
            this.logger.LogInformation("User Created");
        }
    }
}