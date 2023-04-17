using System;

namespace Sat.Recruitment.Api.Domain.UserTypes
{
    public class Normal : UserType
    {
        public override decimal GetMoney(decimal baseMoney)
        {
            if (baseMoney > 100)
            {
                return baseMoney + (baseMoney * Convert.ToDecimal(0.12));
            }
            else if (baseMoney < 100 && baseMoney > 10)
            {
                return baseMoney + (baseMoney * Convert.ToDecimal(0.08));
            } 
            else 
            {
                return baseMoney;
            }
        }
    }
}