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
            var emailParts = this.value.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            var atIndex = emailParts[0].IndexOf("+", StringComparison.Ordinal);
            emailParts[0] = atIndex < 0 ? emailParts[0].Replace(".", "") : emailParts[0].Remove(atIndex).Replace(".", "");

            return string.Join("@", new string[] { emailParts[0], emailParts[1] });
        }
    }
}