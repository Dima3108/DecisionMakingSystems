using FLS;
using FLS.MembershipFunctions;

namespace FuzzyRestaurantEvaluation.Models
{
    public static class N_Output
    {
        public static LinguisticVariable Create(out IMembershipFunction оченьНизкая, out IMembershipFunction низкая, out IMembershipFunction средняя, out IMembershipFunction высокая, out IMembershipFunction оченьВысокая)
        {
            var n = new LinguisticVariable("N_выход");
            оченьНизкая = n.MembershipFunctions.AddTrapezoid("Очень_низкая", 0, 0, 0.5, 1);
            низкая = n.MembershipFunctions.AddTriangle("Низкая", 0.5, 1, 1.5);
            средняя = n.MembershipFunctions.AddTriangle("Средняя", 1.5, 2, 2.5);
            высокая = n.MembershipFunctions.AddTriangle("Высокая", 2.5, 3, 3.5);
            оченьВысокая = n.MembershipFunctions.AddTrapezoid("Очень_высокая", 3.5, 4, 4, 4);
            return n;
        }
    }
}