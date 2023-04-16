using System;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

using Sat.Recruitment.Api.Domain;

using Xunit;

namespace Sat.Recruitment.Test.Domain
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class AssertionConcernTest
    {
        [Fact]
        public void ItMustAssertArgumentNotNull()
        {
            var errorMessage = "some-message";
            var exception = Record.Exception(() => AssertionConcern.AssertArgumentNotNull(null, errorMessage));
            Assert.IsType<InvalidOperationException>(exception);
            Assert.Equal(errorMessage, exception.Message);

            var nonException = Record.Exception(() => AssertionConcern.AssertArgumentNotNull(new {}, errorMessage));
            Assert.Null(nonException);
        }
    }
}
