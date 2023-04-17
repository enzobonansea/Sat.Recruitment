using System;
using System.Dynamic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Sat.Recruitment.Api.Domain;
using Sat.Recruitment.Api.Application;
using Sat.Recruitment.Api.Controllers;

using Xunit;


namespace Sat.Recruitment.Test.Application
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class CreateUserUseCaseTest
    {
        [Fact]
        public async Task ItShouldCreateUserIfNotExists()
        {
            var useCase = new CreateUserUseCase(new UserNotExistsRepository());
            var exception = await Record.ExceptionAsync(() => useCase.ExecuteAsync(Utils.RandomUser()));
            Assert.Null(exception);
        }

        [Fact]
        public async Task ItShouldNotCreateUserIfExists()
        {
            var useCase = new CreateUserUseCase(new UserExistsRepository());
            var exception = await Record.ExceptionAsync(() => useCase.ExecuteAsync(Utils.RandomUser()));
            Assert.IsType<UserDuplicatedException>(exception);
        }
    }

    class UserExistsRepository : IUsersRepository 
    {
        public Task<bool> ExistsAsync(User user) 
        {
            return Task.FromResult(true);
        }

        public Task SaveAsync(User user) 
        {
            return Task.CompletedTask;
        }
    }

    class UserNotExistsRepository : IUsersRepository 
    {
        public Task<bool> ExistsAsync(User user) 
        {
            return Task.FromResult(false);
        }

        public Task SaveAsync(User user) 
        {
            return Task.CompletedTask;
        }
    }
}
