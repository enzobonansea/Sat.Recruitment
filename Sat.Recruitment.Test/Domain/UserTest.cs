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
            var anotherUser = new User("Mike", this.user.Email, "Av. Juan G", "+349 1122354215", "Normal", decimal.Parse("124"));
            var isDuplicated = this.user.IsDuplicated(anotherUser);
            Assert.Equal(true, isDuplicated);
        }
    }
}
