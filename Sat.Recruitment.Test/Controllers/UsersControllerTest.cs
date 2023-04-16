using System;
using System.Dynamic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Sat.Recruitment.Api.Domain;
using Sat.Recruitment.Api.Application;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Infrastructure;

using Xunit;


namespace Sat.Recruitment.Test.Controllers
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UsersControllerTest
    {
        private readonly UsersController userController = new UsersController(new CreateUserUseCase(new FileUsersRepository()), new CreateUserRequestValidator());

        [Fact]
        public async Task Test1()
        {
            var result = await userController.CreateUser("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");

            Assert.Equal(true, result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }

        [Fact]
        public async Task Test2()
        {
            var result = await userController.CreateUser("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");

            Assert.Equal(false, result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }
    }
}
