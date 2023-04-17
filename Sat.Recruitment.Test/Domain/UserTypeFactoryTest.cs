using System;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

using Sat.Recruitment.Api.Domain;
using Sat.Recruitment.Api.Domain.UserTypes;

using Xunit;

namespace Sat.Recruitment.Test.Domain
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserTypeFactoryTest
    {
        [Fact]
        public void ItMustReturnACorrectInstanceBasedOnType()
        {
            Assert.IsType<Normal>(UserTypeFactory.Create("Normal"));
            Assert.IsType<Premium>(UserTypeFactory.Create("Premium"));
            Assert.IsType<SuperUser>(UserTypeFactory.Create("SuperUser"));
        }

        [Fact]
        public void ItMustThrowOnInvalidType()
        {
            var exception = Record.Exception(() => UserTypeFactory.Create("invalid-type"));
            Assert.IsType<InvalidOperationException>(exception);
            Assert.Equal(UserTypeFactory.TypeIsInvalidErrorMessage, exception.Message);
        }
    }
}
