using FLS;
using FLS.MembershipFunctions;

namespace FuzzyRestaurantEvaluation.Models
{
    public static class E_Parking
    {
        public static LinguisticVariable Create(out IMembershipFunction бесплатная, out IMembershipFunction платная, out IMembershipFunction отсутствует)
        {
            var e = new LinguisticVariable("E_уровень");
            бесплатная = e.MembershipFunctions.AddTrapezoid("Бесплатная", 0, 0, 0.5, 1);
            платная = e.MembershipFunctions.AddTriangle("Платная", 0.5, 1, 1.5);
            отсутствует = e.MembershipFunctions.AddTrapezoid("Отсутствует", 1.5, 2, 2, 2);
            return e;
        }
    }
}