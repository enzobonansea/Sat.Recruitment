using System;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

using Sat.Recruitment.Api.Domain;

using Xunit;

namespace Sat.Recruitment.Test.Domain
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class EmailTest
    {
        [Fact]
        public void IsMustNormalizeEmails()
        {
            var email = new Email("some.thing@gmail.com");
            Assert.Equal("something@gmail.com", email.Normalize());

            email = new Email("something+123@gmail.com");
            Assert.Equal("something@gmail.com", email.Normalize());

            email = new Email("some.thing+123@gmail.com");
            Assert.Equal("something@gmail.com", email.Normalize());
        }
    }
}
