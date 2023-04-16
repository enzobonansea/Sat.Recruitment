using System;
using System.Linq;
using Xunit;

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
    }
}