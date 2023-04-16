using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

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

        public UsersController(CreateUserUseCase createUserUseCase, CreateUserRequestValidator createUserRequestValidator)
        {
            this.createUserUseCase = createUserUseCase;
            this.createUserRequestValidator = createUserRequestValidator;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(string name, string email, string address, string phone, string userType, string money)
        {
            try
            {
                createUserRequestValidator.Execute(name, email, address, phone);

                await this.createUserUseCase.Execute(new User(name, email, address, phone, userType, decimal.Parse(money)));

                return new Result()
                {
                    IsSuccess = true,
                    Errors = "User Created"
                };
            }
            catch (UserValidationException exception)
            {
                return new Result()
                {
                    IsSuccess = false,
                    Errors = string.Join(" ", exception.Errors)
                };
            }
            catch (UserDuplicatedException)
            {
                return new Result()
                {
                    IsSuccess = false,
                    Errors = "The user is duplicated"
                };
            }
        }
    }
}
