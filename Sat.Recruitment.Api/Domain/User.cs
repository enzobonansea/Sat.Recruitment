using System;

using Sat.Recruitment.Api.Domain.UserTypes;

namespace Sat.Recruitment.Api.Domain
{
    public class User
    {
        private readonly decimal money;
        private readonly Email email;

        public string Name { get; private set; }
        public string Email { get => this.email.Normalize(); }
        public string Address { get; private set; }
        public string Phone { get; private set; }
        public UserType UserType { get; private set; }
        public decimal Money { get => this.UserType.GetMoney(this.money); }

        public User(string name, string email, string address, string phone, string userType, decimal money)
        { 
            UserValidator.Execute(name, email, address, phone);

            this.Name = name;
            this.Address = address;
            this.Phone = phone;
            this.UserType = UserTypeFactory.Create(userType);
            this.money = money;
            this.email = new Email(email);
        }

        public bool IsDuplicated(User anotherUser) 
        {
            return this.Email == anotherUser.Email 
                || this.Phone == anotherUser.Phone
                || this.Name == anotherUser.Name
                || this.Address == anotherUser.Address;
        }
    }
}