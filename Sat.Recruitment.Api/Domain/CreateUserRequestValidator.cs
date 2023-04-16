using System;

namespace Sat.Recruitment.Api.Domain
{
    public class CreateUserRequestValidator
    {
        public const string NameIsRequiredErrorMessage = "The name is required";
        public const string EmailIsRequiredErrorMessage = "The email is required";
        public const string AddressIsRequiredErrorMessage = "The address is required";
        public const string PhoneIsRequiredErrorMessage = "The phone is required";

        public void Execute(string name, string email, string address, string phone)
        {
            
        }
    }
}