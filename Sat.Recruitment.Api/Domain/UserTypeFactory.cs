using System;
using System.Linq;
using System.Reflection;

using Sat.Recruitment.Api.Domain.UserTypes;

namespace Sat.Recruitment.Api.Domain
{
    public static class UserTypeFactory 
    {
        public const string TypeIsInvalidErrorMessage = "The provided type is invalid";

        public static UserType Create(string aType)
        {
            var objectType = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(type => type.Name == aType);
            
            return objectType is null ? null : (UserType)Activator.CreateInstance(objectType);
        }
    }
}