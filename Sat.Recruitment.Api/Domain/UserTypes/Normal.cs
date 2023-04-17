using System;

namespace Sat.Recruitment.Api.Domain.UserTypes
{
    public class Normal : UserType
    {
        public override decimal GetMoney(decimal baseMoney)
        {
            return baseMoney;
        }
    }
}