using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

using Sat.Recruitment.Api;
using Sat.Recruitment.Api.Domain;
using Sat.Recruitment.Api.Application;

namespace Sat.Recruitment.Api.Controllers
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Errors { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly CreateUserRequestValidator createUserRequestValidator;
        private readonly CreateUserUseCase createUserUseCase;
        private readonly IFeatureManager featureManager;

        public UsersController(CreateUserUseCase createUserUseCase, CreateUserRequestValidator createUserRequestValidator, IFeatureManager featureManager)
        {
            this.createUserUseCase = createUserUseCase;
            this.createUserRequestValidator = createUserRequestValidator;
            this.featureManager = featureManager;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<object> CreateUserAsync(string name, string email, string address, string phone, string userType, string money)
        {
            try
            {
                createUserRequestValidator.Execute(name, email, address, phone);

                await this.createUserUseCase.ExecuteAsync(new User(name, email, address, phone, userType, decimal.Parse(money)));

                return await this.UserCreated();
            }
            catch (UserValidationException exception)
            {
                return await this.UserNotCreated(string.Join(" ", exception.Errors));
            }
            catch (UserDuplicatedException)
            {
                return await this.UserNotCreated("The user is duplicated");
            }
        }

        private async Task<object> UserCreated()
        {
            if (await featureManager.IsEnabledAsync(FeatureFlags.ComplyWithRest))
            {
                // Return 201 Created
                throw new NotImplementedException();
            }
            else
            {
                return new Result()
                {
                    IsSuccess = true,
                    Errors = "User Created"
                };
            }
        }

        private async Task<object> UserNotCreated(string errors)
        {
            if (await featureManager.IsEnabledAsync(FeatureFlags.ComplyWithRest))
            {
                // Return 400 BadRequest
                throw new NotImplementedException();
            }
            else
            {
                return new Result()
                {
                    IsSuccess = false,
                    Errors = errors
                };
            }
        }
    }
}
