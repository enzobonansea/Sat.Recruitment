using System;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

using Sat.Recruitment.Api.Domain;
using Sat.Recruitment.Api.Domain.UserTypes;

using Xunit;

namespace Sat.Recruitment.Test.Domain
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class PremiumTest
    {
        [Fact]
        public void ItMustHave200PercentGiftIfHasMoreThanUSD100()
        {
            var baseMoney = (decimal)101;
            var userType = new Premium();
            Assert.Equal(true, baseMoney * 3 == userType.GetMoney(baseMoney));
        }

        [Fact]
        public void ItMustHaveNotGiftIfHasLessThanOrEqualToUSD100()
        {
            var baseMoney =(decimal)100;
            var userType = new Premium();
            Assert.Equal(true, baseMoney == userType.GetMoney(baseMoney));

            baseMoney =(decimal)99;
            Assert.Equal(true, baseMoney == userType.GetMoney(baseMoney));
        }
    }
}