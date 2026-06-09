using Accord.Fuzzy;

namespace FuzzyRestaurantEvaluation.Models
{
    public static class N_Output
    {
        public static LinguisticVariable Create()
        {
            LinguisticVariable n = new LinguisticVariable("N_Оценка", 0f, 2f);

            FuzzySet низкая = new FuzzySet("Низкая", new TrapezoidalFunction(0f, 0f, 0.4f, 0.6f));
            FuzzySet средняя = new FuzzySet("Средняя", new TrapezoidalFunction(0.4f, 0.6f, 1.4f, 1.6f));
            FuzzySet высокая = new FuzzySet("Высокая", new TrapezoidalFunction(1.4f, 1.6f, 2f, 2f));

            n.AddLabel(низкая);
            n.AddLabel(средняя);
            n.AddLabel(высокая);

            return n;
        }
    }
}