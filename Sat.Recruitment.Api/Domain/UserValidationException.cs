using System;
using System.Collections.Generic;

namespace Sat.Recruitment.Api.Domain
{
    public class UserValidationException : Exception
    {
        public IEnumerable<string> Errors { get; private set; }
        
        public UserValidationException(IEnumerable<string> errors)
        {
            this.Errors = errors;
        }
    }
}