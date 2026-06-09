using System;
using FuzzyRestaurantEvaluation.Rules;

namespace FuzzyRestaurantEvaluation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=".PadRight(70, '='));
            Console.WriteLine("НЕЧЁТКАЯ СИСТЕМА ОЦЕНКИ ЗАВЕДЕНИЙ");
            Console.WriteLine("Правила Максима (строки 1-19)");
            Console.WriteLine("Вход: A(D_код), C(E_код), E(F_код)");
            Console.WriteLine("Выход: L (0=низкая, 1=средняя, 2=высокая)");
            Console.WriteLine("=".PadRight(70, '='));
            Console.WriteLine();

            MaximRules.Initialize();
            Console.WriteLine("✅ Система нечётких правил инициализирована\n");

            Console.WriteLine("ТЕСТОВЫЕ ПРИМЕРЫ:");
            Console.WriteLine("-".PadRight(70, '-'));

            var testCases = new[]
            {
                new { A = 0.0, C = 0.0, E = 1.0, Name = "Низкая конкуренция (0), Бесплатная парковка (0), Вход с улицы (1)" },
                new { A = 1.0, C = 1.0, E = 0.0, Name = "Средняя конкуренция (1), Платная парковка (1), Вход через ТЦ (0)" },
                new { A = 2.0, C = 2.0, E = 3.0, Name = "Высокая конкуренция (2), Нет парковки (2), Вход через здание (3)" },
                new { A = 0.0, C = 0.0, E = 2.0, Name = "Низкая конкуренция (0), Бесплатная парковка (0), Вход через арку (2)" },
                new { A = 2.0, C = 1.0, E = 1.0, Name = "Высокая конкуренция (2), Платная парковка (1), Вход с улицы (1)" },
            };

            foreach (var test in testCases)
            {
                double result = MaximRules.Evaluate(test.A, test.C, test.E);
                int intResult = MaximRules.EvaluateInt(test.A, test.C, test.E);
                string resultText = intResult == 0 ? "НИЗКАЯ" : (intResult == 1 ? "СРЕДНЯЯ" : "ВЫСОКАЯ");

                Console.WriteLine($"\n{test.Name}");
                Console.WriteLine($"  A={test.A}, C={test.C}, E={test.E} → Прогноз = {result:F2} → {resultText}");
            }

            Console.WriteLine("\n" + "=".PadRight(70, '='));
            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}