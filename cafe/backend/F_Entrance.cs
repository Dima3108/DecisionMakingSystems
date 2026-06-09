using Accord.Fuzzy;

namespace FuzzyRestaurantEvaluation.Models
{
    public static class F_Entrance
    {
        public static LinguisticVariable Create()
        {
            LinguisticVariable f = new LinguisticVariable("F_Вход", 0f, 3f);

            FuzzySet через_ТЦ = new FuzzySet("Через_ТЦ", new TrapezoidalFunction(0f, 0f, 0.4f, 0.6f));
            FuzzySet с_улицы = new FuzzySet("С_улицы", new TrapezoidalFunction(0.4f, 0.6f, 1.4f, 1.6f));
            FuzzySet через_арку = new FuzzySet("Через_арку", new TrapezoidalFunction(1.4f, 1.6f, 2.4f, 2.6f));
            FuzzySet через_здание = new FuzzySet("Через_здание", new TrapezoidalFunction(2.4f, 2.6f, 3f, 3f));

            f.AddLabel(через_ТЦ);
            f.AddLabel(с_улицы);
            f.AddLabel(через_арку);
            f.AddLabel(через_здание);

            return f;
        }
    }
}