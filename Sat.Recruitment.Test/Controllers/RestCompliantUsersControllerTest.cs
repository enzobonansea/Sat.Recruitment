using System;
using System.Dynamic;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.FeatureManagement;


using Sat.Recruitment.Api.Domain;
using Sat.Recruitment.Api.Application;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Infrastructure;

using Xunit;

namespace Sat.Recruitment.Test.Controllers
{
    class RestCompliantBehaviorFeatureManager : IFeatureManager
    {
        public IAsyncEnumerable<string> GetFeatureNamesAsync()
        {
            return null;
        }

        public Task<bool> IsEnabledAsync(string feature)
        {
            return Task.FromResult(true);
        }

        public Task<bool> IsEnabledAsync<TContext>(string feature, TContext context)
        {
            return Task.FromResult(true);
        }
    }
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class RestCompliantUsersControllerTest
    {
        private readonly UsersController userController = new UsersController(new CreateUserUseCase(new FileUsersRepository(), NullLogger<CreateUserUseCase>.Instance), new CreateUserRequestValidator(), new RestCompliantBehaviorFeatureManager());

        [Fact]
        public async Task ItMustCreateANewUser()
        {
            var result = (ObjectResult)await userController.CreateUserAsync("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");
            Assert.Equal(result.StatusCode, 201);
        }

        [Fact]
        public async Task ItMustNotCreateAnExistingUser()
        {
            var result = (ObjectResult)await userController.CreateUserAsync("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");
            Assert.Equal(result.StatusCode, 400);
            Assert.Equal("The user is duplicated", this.ReadErrors(result));
        }

        [Fact]
        public async Task ItMustNotCreateAUserWithoutName()
        {
            var result = (ObjectResult)await userController.CreateUserAsync(null, Utils.RandomMail(), Utils.RandomString(7), Utils.RandomString(7), "Normal", "124");
            Assert.Equal(result.StatusCode, 400);
            Assert.Equal(true, this.ReadErrors(result).Contains(CreateUserRequestValidator.NameIsRequiredErrorMessage));
        }

        [Fact]
        public async Task ItMustNotCreateAUserWithoutEmail()
        {
            var result = (ObjectResult)await userController.CreateUserAsync(Utils.RandomString(7), null, Utils.RandomString(7), Utils.RandomString(7), "Normal", "124");
            Assert.Equal(result.StatusCode, 400);
            Assert.Equal(true, this.ReadErrors(result).Contains(CreateUserRequestValidator.EmailIsRequiredErrorMessage));
        }

        [Fact]
        public async Task ItMustNotCreateAUserWithoutAddress()
        {
            var result = (ObjectResult)await userController.CreateUserAsync(Utils.RandomString(7), Utils.RandomMail(), null, Utils.RandomString(7), "Normal", "124");
            Assert.Equal(result.StatusCode, 400);
            Assert.Equal(true,this.ReadErrors(result).Contains(CreateUserRequestValidator.AddressIsRequiredErrorMessage));
        }

        [Fact]
        public async Task ItMustNotCreateAUserWithoutPhone()
        {
            var result = (ObjectResult)await userController.CreateUserAsync(Utils.RandomString(7), Utils.RandomMail(), Utils.RandomString(7), null, "Normal", "124");
            Assert.Equal(result.StatusCode, 400);
            Assert.Equal(true, this.ReadErrors(result).Contains(CreateUserRequestValidator.PhoneIsRequiredErrorMessage));
        }

        private string ReadErrors(ObjectResult result)
        {
            return result.Value.GetType().GetProperty("Errors").GetValue(result.Value, null).ToString();
        }
    }
}
