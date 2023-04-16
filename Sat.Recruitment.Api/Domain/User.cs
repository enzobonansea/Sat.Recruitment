using System;

namespace Sat.Recruitment.Api.Domain
{
    public class User
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Address { get; private set; }
        public string Phone { get; private set; }
        public string UserType { get; private set; }
        public decimal Money { get; private set; }

        public User(string name, string email, string address, string phone, string userType, decimal money)
        {
            this.Name = name;
            this.Email = email;
            this.Address = address;
            this.Phone = phone;
            this.UserType = userType;
            this.Money = money;

            if (this.UserType == "Normal")
            {
                if (money > 100)
                {
                    var percentage = Convert.ToDecimal(0.12);
                    //If new user is normal and has more than USD100
                    var gif = money * percentage;
                    this.Money = this.Money + gif;
                }
                if (money < 100)
                {
                    if (money > 10)
                    {
                        var percentage = Convert.ToDecimal(0.8);
                        var gif = money * percentage;
                        this.Money = this.Money + gif;
                    }
                }
            }
            if (this.UserType == "SuperUser")
            {
                if (money > 100)
                {
                    var percentage = Convert.ToDecimal(0.20);
                    var gif = money * percentage;
                    this.Money = this.Money + gif;
                }
            }
            if (this.UserType == "Premium")
            {
                if (money > 100)
                {
                    var gif = money * 2;
                    this.Money = this.Money + gif;
                }
            }

            //Normalize email
            var aux = this.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            this.Email = string.Join("@", new string[] { aux[0], aux[1] });
        }

        public bool IsDuplicated(User anotherUser) 
        {
            return this.Email == anotherUser.Email || this.Phone == anotherUser.Phone;
        }
    }
}