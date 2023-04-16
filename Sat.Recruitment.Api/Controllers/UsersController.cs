using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Sat.Recruitment.Api.Domain;

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

        private readonly List<User> _users = new List<User>();
        public UsersController()
        {
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(string name, string email, string address, string phone, string userType, string money)
        {
            var errors = "";

            ValidateErrors(name, email, address, phone, ref errors);

            if (errors != null && errors != "")
                return new Result()
                {
                    IsSuccess = false,
                    Errors = errors
                };

            var newUser = new User(name, email, address, phone, userType, decimal.Parse(money));


            var reader = ReadUsersFromFile();

            while (reader.Peek() >= 0)
            {
                var line = await reader.ReadLineAsync();
                var user = new User(
                    name: line.Split(',')[0].ToString(),
                    email: line.Split(',')[1].ToString(),
                    phone: line.Split(',')[2].ToString(),
                    address: line.Split(',')[3].ToString(),
                    userType: line.Split(',')[4].ToString(),
                    money: decimal.Parse(line.Split(',')[5].ToString()));
                _users.Add(user);
            }
            reader.Close();

            var isDuplicated = _users.Any(user => newUser.IsDuplicated(user));

            if (!isDuplicated)
            {
                Debug.WriteLine("User Created");

                return new Result()
                {
                    IsSuccess = true,
                    Errors = "User Created"
                };
            }
            else
            {
                Debug.WriteLine("The user is duplicated");

                return new Result()
                {
                    IsSuccess = false,
                    Errors = "The user is duplicated"
                };
            }
        }

        //Validate errors
        private void ValidateErrors(string name, string email, string address, string phone, ref string errors)
        {
            if (name == null)
                //Validate if Name is null
                errors = "The name is required";
            if (email == null)
                //Validate if Email is null
                errors = errors + " The email is required";
            if (address == null)
                //Validate if Address is null
                errors = errors + " The address is required";
            if (phone == null)
                //Validate if Phone is null
                errors = errors + " The phone is required";
        }
    }
}
