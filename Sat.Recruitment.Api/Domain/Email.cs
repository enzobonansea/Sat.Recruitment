using System;

namespace Sat.Recruitment.Api.Domain
{
    public class Email 
    {
        private readonly string value;

        public Email(string value)
        {
            this.value = value;
        }

        public string Normalize()
        {
            return value;
        }
    }
}