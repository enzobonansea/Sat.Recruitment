using System;

namespace Sat.Recruitment.Api.Domain.UserTypes
{
    public abstract class UserType
    {
        public abstract decimal GetMoney(decimal baseMoney);
    }
}