using System;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

using Sat.Recruitment.Api.Domain;
using Sat.Recruitment.Api.Domain.UserTypes;

using Xunit;

namespace Sat.Recruitment.Test.Domain
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class NormalTest
    {
        [Fact]
        public void ItMustHave12PercentGiftIfHasMoreThanUSD100()
        {
            var baseMoney = (decimal)101;
            var userType = new Normal();
            Assert.Equal(true, baseMoney * (decimal)1.12 == userType.GetMoney(baseMoney));
        }

        [Fact]
        public void ItMustHave8PercentGiftIfHasLessThanUSD100AndMoreThanUSD10()
        {
            var baseMoney =(decimal)99;
            var userType = new Normal();
            Assert.Equal(true, baseMoney * (decimal)1.08 == userType.GetMoney(baseMoney));
        }

        [Fact]
        public void ItMustHaveNotGiftIfHasExactlyUSD100OrLessThan10USD()
        {
            var baseMoney =(decimal)100;
            var userType = new Normal();
            Assert.Equal(true, baseMoney == userType.GetMoney(baseMoney));

            baseMoney =(decimal)9;
            Assert.Equal(true, baseMoney == userType.GetMoney(baseMoney));
        }
    }
}
