using System;

namespace Sat.Recruitment.Api.Domain.UserTypes
{
    public class SuperUser : UserType
    {
        public override decimal GetMoney(decimal baseMoney)
        {
            return baseMoney > 100 
                ? baseMoney + (baseMoney * Convert.ToDecimal(0.20))
                : baseMoney;
        }
    }
}