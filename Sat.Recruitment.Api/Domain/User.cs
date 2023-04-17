using System;

using Sat.Recruitment.Api.Domain.UserTypes;

namespace Sat.Recruitment.Api.Domain
{
    public class User
    {
        private readonly decimal money;

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Address { get; private set; }
        public string Phone { get; private set; }
        public UserType UserType { get; private set; }
        public decimal Money { get => this.UserType.GetMoney(this.money); }

        public User(string name, string email, string address, string phone, string userType, decimal money)
        {
            AssertionConcern.AssertArgumentNotNull(name, CreateUserRequestValidator.NameIsRequiredErrorMessage);
            AssertionConcern.AssertArgumentNotNull(email, CreateUserRequestValidator.EmailIsRequiredErrorMessage);
            AssertionConcern.AssertArgumentNotNull(address, CreateUserRequestValidator.AddressIsRequiredErrorMessage);
            AssertionConcern.AssertArgumentNotNull(phone, CreateUserRequestValidator.PhoneIsRequiredErrorMessage);

            this.Name = name;
            this.Email = email;
            this.Address = address;
            this.Phone = phone;
            this.UserType = UserTypeFactory.Create(userType);
            this.money = money;

            //Normalize email
            var aux = this.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            this.Email = string.Join("@", new string[] { aux[0], aux[1] });
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