using Accord.Fuzzy;

namespace FuzzyRestaurantEvaluation.Models
{
    public static class D_Competitors
    {
        public static LinguisticVariable Create()
        {

            LinguisticVariable d = new LinguisticVariable("D_Конкуренты", 0f, 2f);

            FuzzySet низкий = new FuzzySet("Низкий", new TrapezoidalFunction(0f, 0f, 0.4f, 0.6f));
            FuzzySet средний = new FuzzySet("Средний", new TrapezoidalFunction(0.4f, 0.6f, 1.4f, 1.6f));
            FuzzySet высокий = new FuzzySet("Высокий", new TrapezoidalFunction(1.4f, 1.6f, 2f, 2f));

            d.AddLabel(низкий);
            d.AddLabel(средний);
            d.AddLabel(высокий);

            return d;
        }
    }
}