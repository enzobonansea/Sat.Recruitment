using System;
using System.Linq;
using Xunit;
using Sat.Recruitment.Api.Domain;

namespace Sat.Recruitment.Test
{
    public static class Utils
    {
        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RandomMail() 
        {
            return RandomString(7) + "@gmail.com"; 
        }

        public static User RandomUser() 
        {
            return new User(RandomString(7), RandomMail(), RandomString(7), RandomString(7), "Normal", decimal.Parse("123"));
        }
    }
}
