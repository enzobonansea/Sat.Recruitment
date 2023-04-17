using System;

namespace Sat.Recruitment.Api.Domain.UserTypes
{
    public class Premium : UserType
    {
        public override decimal GetMoney(decimal baseMoney)
        {
            return baseMoney;
        }
    }
}