using FLS;
using FLS.MembershipFunctions;

namespace FuzzyRestaurantEvaluation.Models
{
    public static class F_Entrance
    {
        public static LinguisticVariable Create(out IMembershipFunction черезТц, out IMembershipFunction сУлицы, out IMembershipFunction черезАрку, out IMembershipFunction черезЗдание)
        {
            var f = new LinguisticVariable("F_уровень");
            черезТц = f.MembershipFunctions.AddTrapezoid("Через_ТЦ", 0, 0, 0.5, 1);
            сУлицы = f.MembershipFunctions.AddTriangle("С_улицы", 0.5, 1, 1.5);
            черезАрку = f.MembershipFunctions.AddTriangle("Через_арку", 1.5, 2, 2.5);
            черезЗдание = f.MembershipFunctions.AddTrapezoid("Через_здание", 2.5, 3, 3, 3);
            return f;
        }
    }
}