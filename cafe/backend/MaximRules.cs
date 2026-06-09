using Accord.Fuzzy;
using FuzzyRestaurantEvaluation.Models;
using System;

namespace FuzzyRestaurantEvaluation.Rules
{
    public static class MaximRules
    {
        private static InferenceSystem _inferenceSystem;

        public static void Initialize()
        {
            Database database = new Database();

            LinguisticVariable D = D_Competitors.Create();
            LinguisticVariable E = E_Parking.Create();
            LinguisticVariable F = F_Entrance.Create();
            LinguisticVariable N = N_Output.Create();

            database.AddVariable(D);
            database.AddVariable(E);
            database.AddVariable(F);
            database.AddVariable(N);

            _inferenceSystem = new InferenceSystem(database, new CentroidDefuzzifier(1000));

            // R1-R8: Низкий + ... → Высокая
            _inferenceSystem.NewRule("R1", "IF D_Конкуренты IS Низкий AND E_Парковка IS Бесплатная AND F_Вход IS Через_ТЦ THEN N_Оценка IS Высокая");
            _inferenceSystem.NewRule("R2", "IF D_Конкуренты IS Низкий AND E_Парковка IS Бесплатная AND F_Вход IS С_улицы THEN N_Оценка IS Высокая");
            _inferenceSystem.NewRule("R3", "IF D_Конкуренты IS Низкий AND E_Парковка IS Бесплатная AND F_Вход IS Через_арку THEN N_Оценка IS Высокая");
            _inferenceSystem.NewRule("R4", "IF D_Конкуренты IS Низкий AND E_Парковка IS Бесплатная AND F_Вход IS Через_здание THEN N_Оценка IS Высокая");
            _inferenceSystem.NewRule("R5", "IF D_Конкуренты IS Низкий AND E_Парковка IS Платная AND F_Вход IS Через_ТЦ THEN N_Оценка IS Высокая");
            _inferenceSystem.NewRule("R6", "IF D_Конкуренты IS Низкий AND E_Парковка IS Платная AND F_Вход IS С_улицы THEN N_Оценка IS Высокая");
            _inferenceSystem.NewRule("R7", "IF D_Конкуренты IS Низкий AND E_Парковка IS Платная AND F_Вход IS Через_арку THEN N_Оценка IS Высокая");
            _inferenceSystem.NewRule("R8", "IF D_Конкуренты IS Низкий AND E_Парковка IS Платная AND F_Вход IS Через_здание THEN N_Оценка IS Высокая");

            // R9-R12: Низкий + Отсутствует → Средняя (R9-R11) / Высокая (R12)
            _inferenceSystem.NewRule("R9", "IF D_Конкуренты IS Низкий AND E_Парковка IS Отсутствует AND F_Вход IS Через_ТЦ THEN N_Оценка IS Средняя");
            _inferenceSystem.NewRule("R10", "IF D_Конкуренты IS Низкий AND E_Парковка IS Отсутствует AND F_Вход IS С_улицы THEN N_Оценка IS Средняя");
            _inferenceSystem.NewRule("R11", "IF D_Конкуренты IS Низкий AND E_Парковка IS Отсутствует AND F_Вход IS Через_арку THEN N_Оценка IS Средняя");
            _inferenceSystem.NewRule("R12", "IF D_Конкуренты IS Низкий AND E_Парковка IS Отсутствует AND F_Вход IS Через_здание THEN N_Оценка IS Высокая");

            // R13-R16: Средний + Бесплатная → Высокая (R13,R15,R16) / Средняя (R14)
            _inferenceSystem.NewRule("R13", "IF D_Конкуренты IS Средний AND E_Парковка IS Бесплатная AND F_Вход IS Через_ТЦ THEN N_Оценка IS Высокая");
            _inferenceSystem.NewRule("R14", "IF D_Конкуренты IS Средний AND E_Парковка IS Бесплатная AND F_Вход IS С_улицы THEN N_Оценка IS Средняя");
            _inferenceSystem.NewRule("R15", "IF D_Конкуренты IS Средний AND E_Парковка IS Бесплатная AND F_Вход IS Через_арку THEN N_Оценка IS Высокая");
            _inferenceSystem.NewRule("R16", "IF D_Конкуренты IS Средний AND E_Парковка IS Бесплатная AND F_Вход IS Через_здание THEN N_Оценка IS Высокая");

            // R17-R19: Средний + Платная → Средняя
            _inferenceSystem.NewRule("R17", "IF D_Конкуренты IS Средний AND E_Парковка IS Платная AND F_Вход IS Через_ТЦ THEN N_Оценка IS Средняя");
            _inferenceSystem.NewRule("R18", "IF D_Конкуренты IS Средний AND E_Парковка IS Платная AND F_Вход IS С_улицы THEN N_Оценка IS Средняя");
            _inferenceSystem.NewRule("R19", "IF D_Конкуренты IS Средний AND E_Парковка IS Платная AND F_Вход IS Через_арку THEN N_Оценка IS Средняя");
        }

        public static double Evaluate(double dCode, double eCode, double fCode)
        {
            if (_inferenceSystem == null)
                Initialize();

            _inferenceSystem.SetInput("D_Конкуренты", (float)dCode);
            _inferenceSystem.SetInput("E_Парковка", (float)eCode);
            _inferenceSystem.SetInput("F_Вход", (float)fCode);

            return (double)_inferenceSystem.Evaluate("N_Оценка");
        }

        public static int EvaluateInt(double dCode, double eCode, double fCode)
        {
            double result = Evaluate(dCode, eCode, fCode);
            return (int)Math.Round(result, MidpointRounding.AwayFromZero);
        }
    }
}