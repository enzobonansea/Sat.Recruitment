using System;
using System.Collections.Generic;

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
            var errors = new List<string>();

            if (name == null) errors.Add(CreateUserRequestValidator.NameIsRequiredErrorMessage);
            if (email == null) errors.Add(CreateUserRequestValidator.EmailIsRequiredErrorMessage);
            if (address == null) errors.Add(CreateUserRequestValidator.AddressIsRequiredErrorMessage);
            if (phone == null) errors.Add(CreateUserRequestValidator.PhoneIsRequiredErrorMessage);

            if (errors.Count > 0) throw new UserValidationException(errors);
        }
    }
}