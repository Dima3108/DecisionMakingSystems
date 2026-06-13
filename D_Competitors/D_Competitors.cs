using FLS;
using FLS.MembershipFunctions;

namespace FuzzyRestaurantEvaluation.Models
{
    public static class D_Competitors
    {
        public static LinguisticVariable Create(out IMembershipFunction низкий, out IMembershipFunction средний, out IMembershipFunction высокий)
        {
            var d = new LinguisticVariable("D_уровень");
            низкий = d.MembershipFunctions.AddTrapezoid("Низкий", 0, 0, 0.5, 1);
            средний = d.MembershipFunctions.AddTriangle("Средний", 0.5, 1, 1.5);
            высокий = d.MembershipFunctions.AddTrapezoid("Высокий", 1.5, 2, 2, 2);
            return d;
        }
    }
}