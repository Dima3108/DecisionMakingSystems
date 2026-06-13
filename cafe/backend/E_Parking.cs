using FLS.Accord.Fuzzy;

namespace FuzzyRestaurantEvaluation.Models
{
    public static class E_Parking
    {
        public static LinguisticVariable Create()
        {
            LinguisticVariable e = new LinguisticVariable("E_Парковка", 0f, 2f);

            FuzzySet бесплатная = new FuzzySet("Бесплатная", new TrapezoidalFunction(0f, 0f, 0.4f, 0.6f));
            FuzzySet платная = new FuzzySet("Платная", new TrapezoidalFunction(0.4f, 0.6f, 1.4f, 1.6f));
            FuzzySet отсутствует = new FuzzySet("Отсутствует", new TrapezoidalFunction(1.4f, 1.6f, 2f, 2f));

            e.AddLabel(бесплатная);
            e.AddLabel(платная);
            e.AddLabel(отсутствует);

            return e;
        }
    }
}