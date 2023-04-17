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
            Assert.IsType<Normal>(UserTypeFactory.Create("Premium"));
            Assert.IsType<Normal>(UserTypeFactory.Create("SuperUser"));
        }
    }
}
