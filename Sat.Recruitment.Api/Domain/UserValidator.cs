using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Sat.Recruitment.Api.Domain
{
    public static class UserValidator
    {
        private const string emailRegex = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

        public const string NameIsRequiredErrorMessage = "The name is required";
        public const string EmailIsRequiredErrorMessage = "The email is required";
        public const string AddressIsRequiredErrorMessage = "The address is required";
        public const string PhoneIsRequiredErrorMessage = "The phone is required";
        public const string EmailIsInvalidErrorMessage = "The email is invalid";

        public static void Execute(string name, string email, string address, string phone)
        {
            var errors = new List<string>();

            if (name == null) errors.Add(UserValidator.NameIsRequiredErrorMessage);
            if (email == null) errors.Add(UserValidator.EmailIsRequiredErrorMessage);
            if (address == null) errors.Add(UserValidator.AddressIsRequiredErrorMessage);
            if (phone == null) errors.Add(UserValidator.PhoneIsRequiredErrorMessage);
            if (email != null && !Regex.IsMatch(email, emailRegex)) errors.Add(UserValidator.EmailIsInvalidErrorMessage);

            if (errors.Count > 0) throw new UserValidationException(errors);
        }
    }
}