using System;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

using Sat.Recruitment.Api.Domain;

using Xunit;

namespace Sat.Recruitment.Test.Domain
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserValidatorTest
    {
        [Fact]
        public void ItMustValidateNonNullName()
        {
            var exception = Record.Exception(() => UserValidator.Execute(null, null, null, null));
            Assert.IsType<UserValidationException>(exception);
            Assert.Equal(true, ((UserValidationException)exception).Errors.Any(error => error == UserValidator.NameIsRequiredErrorMessage));
        }

        [Fact]
        public void ItMustValidateNonNullEmail()
        { 
            var exception = Record.Exception(() => UserValidator.Execute("some-name", null, null, null));
            Assert.IsType<UserValidationException>(exception);
            Assert.Equal(true, ((UserValidationException)exception).Errors.Any(error => error == UserValidator.EmailIsRequiredErrorMessage));  
        }

        [Fact]
        public void ItMustValidateNonNullAddress()
        {   
            var exception = Record.Exception(() => UserValidator.Execute("some-name", "some-email", null, null));
            Assert.IsType<UserValidationException>(exception);
            Assert.Equal(true, ((UserValidationException)exception).Errors.Any(error => error == UserValidator.AddressIsRequiredErrorMessage));
        }

        [Fact]
        public void ItMustValidateNonNullPhone()
        {   
            var exception = Record.Exception(() => UserValidator.Execute("some-name", "some-email", "some-address", null));
            Assert.IsType<UserValidationException>(exception);
            Assert.Equal(true, ((UserValidationException)exception).Errors.Any(error => error == UserValidator.PhoneIsRequiredErrorMessage));
        }
    }
}
