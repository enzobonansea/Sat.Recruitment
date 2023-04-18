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
    class LegacyBehaviorFeatureManager : IFeatureManager
    {
        public IAsyncEnumerable<string> GetFeatureNamesAsync()
        {
            return null;
        }

        public Task<bool> IsEnabledAsync(string feature)
        {
            return Task.FromResult(false);
        }

        public Task<bool> IsEnabledAsync<TContext>(string feature, TContext context)
        {
            return Task.FromResult(false);
        }
    }
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class LegacyUsersControllerTest
    {
        private readonly UsersController userController = new UsersController(new CreateUserUseCase(new FileUsersRepository(), NullLogger<CreateUserUseCase>.Instance), new CreateUserRequestValidator(), new LegacyBehaviorFeatureManager());

        [Fact]
        public async Task ItMustCreateANewUser()
        {
            var result = (Result)await userController.CreateUserAsync("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");

            Assert.Equal(true, result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }

        [Fact]
        public async Task ItMustNotCreateAnExistingUser()
        {
            var result = (Result)await userController.CreateUserAsync("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");

            Assert.Equal(false, result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }

        [Fact]
        public async Task ItMustNotCreateAUserWithoutName()
        {
            var result = (Result)await userController.CreateUserAsync(null, Utils.RandomMail(), Utils.RandomString(7), Utils.RandomString(7), "Normal", "124");

            Assert.Equal(false, result.IsSuccess);
            Assert.Equal(true, result.Errors.Contains(CreateUserRequestValidator.NameIsRequiredErrorMessage));
        }

        [Fact]
        public async Task ItMustNotCreateAUserWithoutEmail()
        {
            var result = (Result)await userController.CreateUserAsync(Utils.RandomString(7), null, Utils.RandomString(7), Utils.RandomString(7), "Normal", "124");

            Assert.Equal(false, result.IsSuccess);
            Assert.Equal(true, result.Errors.Contains(CreateUserRequestValidator.EmailIsRequiredErrorMessage));
        }

        [Fact]
        public async Task ItMustNotCreateAUserWithoutAddress()
        {
            var result = (Result)await userController.CreateUserAsync(Utils.RandomString(7), Utils.RandomMail(), null, Utils.RandomString(7), "Normal", "124");

            Assert.Equal(false, result.IsSuccess);
            Assert.Equal(true, result.Errors.Contains(CreateUserRequestValidator.AddressIsRequiredErrorMessage));
        }

        [Fact]
        public async Task ItMustNotCreateAUserWithoutPhone()
        {
            var result = (Result)await userController.CreateUserAsync(Utils.RandomString(7), Utils.RandomMail(), Utils.RandomString(7), null, "Normal", "124");

            Assert.Equal(false, result.IsSuccess);
            Assert.Equal(true, result.Errors.Contains(CreateUserRequestValidator.PhoneIsRequiredErrorMessage));
        }
    }
}
