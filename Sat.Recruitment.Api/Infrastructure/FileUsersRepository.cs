using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using Sat.Recruitment.Api.Domain;
using Sat.Recruitment.Api.Application;

namespace Sat.Recruitment.Api.Infrastructure
{
    public class FileUsersRepository : IUsersRepository
    {
        private readonly string filePath;

        public FileUsersRepository()
        {
            this.filePath = Path.Join(Directory.GetCurrentDirectory(), "Files", "Users.txt");
        }
        
        public async Task<bool> ExistsAsync(User aUser)
        {
            var allUsers = await this.GetAllAsync();

            return allUsers.Any(anotherUser => aUser.IsDuplicated(anotherUser));
        }

        public Task SaveAsync(User user)
        {
            var userRow = $"{user.Name},{user.Email},{user.Phone},{user.Address},{user.UserType},{user.Money}" + Environment.NewLine;
            System.IO.File.AppendAllText(this.filePath, userRow);
            
            return Task.CompletedTask;
        }

        private async Task<List<User>> GetAllAsync() 
        {
            var users = new List<User>();

            using var fileStream = new FileStream(this.filePath, FileMode.Open);
            using var reader = new StreamReader(fileStream);
            while (reader.Peek() >= 0)
            {
                var line = await reader.ReadLineAsync();
                Console.WriteLine(line);
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
    }
}