using System;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

using Sat.Recruitment.Api.Domain;

using Xunit;

namespace Sat.Recruitment.Test.Domain
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class CreateUserRequestValidatorTest
    {
        private readonly CreateUserRequestValidator validator = new CreateUserRequestValidator();

        [Fact]
        public void ItMustValidateNonNullName()
        {
            var exception = Record.Exception(() => validator.Execute("", "", "", ""));
            Assert.IsType<UserValidationException>(exception);
            Assert.Equal(true, ((UserValidationException)exception).Errors.Any(error => error == CreateUserRequestValidator.NameIsRequiredErrorMessage));
        }

        [Fact]
        public void ItMustValidateNonNullEmail()
        { 
            var exception = Record.Exception(() => validator.Execute("some-name", "", "", ""));
            Assert.IsType<UserValidationException>(exception);
            Assert.Equal(true, ((UserValidationException)exception).Errors.Any(error => error == CreateUserRequestValidator.EmailIsRequiredErrorMessage));  
        }

        [Fact]
        public void ItMustValidateNonNullAddress()
        {   
            var exception = Record.Exception(() => validator.Execute("some-name", "some-email", "", ""));
            Assert.IsType<UserValidationException>(exception);
            Assert.Equal(true, ((UserValidationException)exception).Errors.Any(error => error == CreateUserRequestValidator.AddressIsRequiredErrorMessage));
        }

        [Fact]
        public void ItMustValidateNonNullPhone()
        {   
            var exception = Record.Exception(() => validator.Execute("some-name", "some-email", "some-address", ""));
            Assert.IsType<UserValidationException>(exception);
            Assert.Equal(true, ((UserValidationException)exception).Errors.Any(error => error == CreateUserRequestValidator.PhoneIsRequiredErrorMessage));
        }
    }
}
