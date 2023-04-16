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
            var anotherUser = new User(Utils.RandomString(4), Utils.RandomMail(), "Av. Juan G", this.user.Phone, "Normal", decimal.Parse("124"));
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
    }
}
