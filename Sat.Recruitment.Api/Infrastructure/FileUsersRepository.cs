using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using Sat.Recruitment.Api.Domain;

namespace Sat.Recruitment.Api.Infrastructure
{
    public class FileUsersRepository : IUsersRepository
    {
        public async Task<bool> Exists(User aUser)
        {
            var allUsers = await this.GetAll();

            return allUsers.Any(anotherUser => aUser.IsDuplicated(anotherUser));
        }

        public Task Save(User user)
        {
            // Add the persistence logic here
            return Task.CompletedTask;
        }

        private async Task<List<User>> GetAll() 
        {
            var users = new List<User>();

            var reader = ReadUsersFromFile();
            while (reader.Peek() >= 0)
            {
                var line = await reader.ReadLineAsync();
                var user = new User(
                    name: line.Split(',')[0].ToString(),
                    email: line.Split(',')[1].ToString(),
                    phone: line.Split(',')[2].ToString(),
                    address: line.Split(',')[3].ToString(),
                    userType: line.Split(',')[4].ToString(),
                    money: decimal.Parse(line.Split(',')[5].ToString()));
                users.Add(user);
            }
            reader.Close();

            return users;
        }

        private StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";
            var fileStream = new FileStream(path, FileMode.Open);
            var reader = new StreamReader(fileStream);

            return reader;
        }
    }
}