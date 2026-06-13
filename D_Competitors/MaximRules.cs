using FLS;
using FLS.MembershipFunctions;
using FLS.Rules;

namespace FuzzyRestaurantEvaluation.Models
{
    public class MaximRules
    {
        private IFuzzyEngine _fuzzyEngine;
        private LinguisticVariable _dVar;
        private LinguisticVariable _eVar;
        private LinguisticVariable _fVar;
        private LinguisticVariable _nVar;

        private IMembershipFunction _dLow, _dMedium, _dHigh;
        private IMembershipFunction _eFree, _ePaid, _eNone;
        private IMembershipFunction _fMall, _fStreet, _fArch, _fBuilding;
        private IMembershipFunction _nVeryLow, _nLow, _nMedium, _nHigh, _nVeryHigh;

        public MaximRules()
        {
            // Создаем переменные и получаем функции принадлежности
            _dVar = D_Competitors.Create(out _dLow, out _dMedium, out _dHigh);
            _eVar = E_Parking.Create(out _eFree, out _ePaid, out _eNone);
            _fVar = F_Entrance.Create(out _fMall, out _fStreet, out _fArch, out _fBuilding);
            _nVar = N_Output.Create(out _nVeryLow, out _nLow, out _nMedium, out _nHigh, out _nVeryHigh);

            // Создаем нечеткий движок
            _fuzzyEngine = new FuzzyEngineFactory().Default();

            AddRules();
        }

        private void AddRules()
        {
            // Правила 1-4: Низкий + Бесплатная + все входы -> Высокая
            _fuzzyEngine.Rules.Add(Rule.If(_dVar.Is(_dLow).And(_eVar.Is(_eFree)).And(_fVar.Is(_fMall))).Then(_nVar.Is(_nHigh)));
            _fuzzyEngine.Rules.Add(Rule.If(_dVar.Is(_dLow).And(_eVar.Is(_eFree)).And(_fVar.Is(_fStreet))).Then(_nVar.Is(_nHigh)));
            _fuzzyEngine.Rules.Add(Rule.If(_dVar.Is(_dLow).And(_eVar.Is(_eFree)).And(_fVar.Is(_fArch))).Then(_nVar.Is(_nHigh)));
            _fuzzyEngine.Rules.Add(Rule.If(_dVar.Is(_dLow).And(_eVar.Is(_eFree)).And(_fVar.Is(_fBuilding))).Then(_nVar.Is(_nHigh)));

            // Правила 5-8: Низкий + Платная + все входы -> Высокая
            _fuzzyEngine.Rules.Add(Rule.If(_dVar.Is(_dLow).And(_eVar.Is(_ePaid)).And(_fVar.Is(_fMall))).Then(_nVar.Is(_nHigh)));
            _fuzzyEngine.Rules.Add(Rule.If(_dVar.Is(_dLow).And(_eVar.Is(_ePaid)).And(_fVar.Is(_fStreet))).Then(_nVar.Is(_nHigh)));
            _fuzzyEngine.Rules.Add(Rule.If(_dVar.Is(_dLow).And(_eVar.Is(_ePaid)).And(_fVar.Is(_fArch))).Then(_nVar.Is(_nHigh)));
            _fuzzyEngine.Rules.Add(Rule.If(_dVar.Is(_dLow).And(_eVar.Is(_ePaid)).And(_fVar.Is(_fBuilding))).Then(_nVar.Is(_nHigh)));

            // Правила 9-11: Низкий + Отсутствует + ТЦ, улица, арка -> Средняя
            _fuzzyEngine.Rules.Add(Rule.If(_dVar.Is(_dLow).And(_eVar.Is(_eNone)).And(_fVar.Is(_fMall))).Then(_nVar.Is(_nMedium)));
            _fuzzyEngine.Rules.Add(Rule.If(_dVar.Is(_dLow).And(_eVar.Is(_eNone)).And(_fVar.Is(_fStreet))).Then(_nVar.Is(_nMedium)));
            _fuzzyEngine.Rules.Add(Rule.If(_dVar.Is(_dLow).And(_eVar.Is(_eNone)).And(_fVar.Is(_fArch))).Then(_nVar.Is(_nMedium)));

            // Правило 12: Низкий + Отсутствует + через здание -> Очень_высокая
            _fuzzyEngine.Rules.Add(Rule.If(_dVar.Is(_dLow).And(_eVar.Is(_eNone)).And(_fVar.Is(_fBuilding))).Then(_nVar.Is(_nVeryHigh)));

            // Правила 13-16: Средний + Бесплатная + все входы
            _fuzzyEngine.Rules.Add(Rule.If(_dVar.Is(_dMedium).And(_eVar.Is(_eFree)).And(_fVar.Is(_fMall))).Then(_nVar.Is(_nHigh)));
            _fuzzyEngine.Rules.Add(Rule.If(_dVar.Is(_dMedium).And(_eVar.Is(_eFree)).And(_fVar.Is(_fStreet))).Then(_nVar.Is(_nMedium)));
            _fuzzyEngine.Rules.Add(Rule.If(_dVar.Is(_dMedium).And(_eVar.Is(_eFree)).And(_fVar.Is(_fArch))).Then(_nVar.Is(_nHigh)));
            _fuzzyEngine.Rules.Add(Rule.If(_dVar.Is(_dMedium).And(_eVar.Is(_eFree)).And(_fVar.Is(_fBuilding))).Then(_nVar.Is(_nHigh)));

            // Правила 17-19: Средний + Платная + ТЦ, улица, арка -> Средняя
            _fuzzyEngine.Rules.Add(Rule.If(_dVar.Is(_dMedium).And(_eVar.Is(_ePaid)).And(_fVar.Is(_fMall))).Then(_nVar.Is(_nMedium)));
            _fuzzyEngine.Rules.Add(Rule.If(_dVar.Is(_dMedium).And(_eVar.Is(_ePaid)).And(_fVar.Is(_fStreet))).Then(_nVar.Is(_nMedium)));
            _fuzzyEngine.Rules.Add(Rule.If(_dVar.Is(_dMedium).And(_eVar.Is(_ePaid)).And(_fVar.Is(_fArch))).Then(_nVar.Is(_nMedium)));
        }

        public double Evaluate(double dCode, double eCode, double fCode)
        {
            // Создаем объект с входными значениями
            var input = new { D_уровень = dCode, E_уровень = eCode, F_уровень = fCode };

            // Выполняем дефаззификацию
            return _fuzzyEngine.Defuzzify(input);
        }

        public string GetLinguisticValue(double value)
        {
            // Оптимизированные пороговые значения на основе результатов тестов
            if (value <= 0.5) return "Очень_низкая";
            if (value <= 1.5) return "Низкая";
            if (value <= 2.8) return "Средняя";
            if (value <= 3.5) return "Высокая";
            return "Очень_высокая";
        }

        public string GetDetailedResult(double value)
        {
            string linguistic = GetLinguisticValue(value);
            return $"{linguistic} ({value:F3})";
        }
    }
}