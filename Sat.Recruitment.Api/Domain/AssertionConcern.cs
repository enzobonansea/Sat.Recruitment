using System;

namespace Sat.Recruitment.Api.Domain
{
    public class AssertionConcern 
    {
        public static void AssertArgumentNotNull(object object1, string message)
        {
            if (object1 == null)
            {
                throw new InvalidOperationException(message);
            }
        }
    }
}