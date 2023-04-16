using System;

using Microsoft.AspNetCore.Mvc;

using Sat.Recruitment.Api.Domain;

using Xunit;

namespace Sat.Recruitment.Test.Domain
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserTest
    {
        private readonly User user = new User("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", decimal.Parse("124"));

        [Fact]
        public void IsDuplicatedWhenSharesEmail()
        {
            var anotherUser = new User(Utils.RandomString(4), this.user.Email, Utils.RandomString(7), Utils.RandomString(10), "Normal", decimal.Parse("124"));
            var isDuplicated = this.user.IsDuplicated(anotherUser);
            Assert.Equal(true, isDuplicated);
        }

        [Fact]
        public void IsDuplicatedWhenSharesPhone()
        {
            var anotherUser = new User(Utils.RandomString(4), Utils.RandomMail(), Utils.RandomString(7), this.user.Phone, "Normal", decimal.Parse("124"));
            var isDuplicated = this.user.IsDuplicated(anotherUser);
            Assert.Equal(true, isDuplicated);
        }

        [Fact]
        public void IsDuplicatedWhenSharesName()
        {
            var anotherUser = new User(this.user.Name, Utils.RandomMail(), Utils.RandomString(7), Utils.RandomString(7), "Normal", decimal.Parse("124"));
            var isDuplicated = this.user.IsDuplicated(anotherUser);
            Assert.Equal(true, isDuplicated);
        }

        [Fact]
        public void IsDuplicatedWhenSharesAddress()
        {
            var anotherUser = new User(Utils.RandomString(4), Utils.RandomMail(), this.user.Address, Utils.RandomString(7), "Normal", decimal.Parse("124"));
            var isDuplicated = this.user.IsDuplicated(anotherUser);
            Assert.Equal(true, isDuplicated);
        }

        [Fact]
        public void ItMustHaveName()
        {
            var exception = Record.Exception(() => new User(null, "", "", "", "", 0));
            Assert.IsType<InvalidOperationException>(exception);
            Assert.Equal(CreateUserRequestValidator.NameIsRequiredErrorMessage, exception.Message);
        }

        [Fact]
        public void ItMustHaveEmail()
        {
            var exception = Record.Exception(() => new User("", null, "", "", "", 0));
            Assert.IsType<InvalidOperationException>(exception);
            Assert.Equal(CreateUserRequestValidator.EmailIsRequiredErrorMessage, exception.Message);
        }

        [Fact]
        public void ItMustHaveAddress()
        {
            var exception = Record.Exception(() => new User("", "", null, "", "", 0));
            Assert.IsType<InvalidOperationException>(exception);
            Assert.Equal(CreateUserRequestValidator.AddressIsRequiredErrorMessage, exception.Message);
        }

        [Fact]
        public void ItMustHavePhone()
        {
            var exception = Record.Exception(() => new User("", "", "", null, "", 0));
            Assert.IsType<InvalidOperationException>(exception);
            Assert.Equal(CreateUserRequestValidator.PhoneIsRequiredErrorMessage, exception.Message);
        }
    }
}
