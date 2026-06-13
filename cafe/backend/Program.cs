using System;
//using FuzzyRestaurantEvaluation.Rules;

namespace FuzzyRestaurantEvaluation
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

// Agregar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173", "http://localhost:3000", "https://joantarazona99.github.io")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Usar CORS
app.UseCors("AllowFrontend");

// Endpoint para recibir datos del formulario
app.MapPost("/api/cafe/analyze", (CafeRequest request) =>
{
    // Imprimir datos en consola
    Console.WriteLine("=== CAFE ANALYSIS REQUEST ===");
    Console.WriteLine($"Customer: {request.Customer}");
    Console.WriteLine($"Cuisine: {request.Cuisine}");
    Console.WriteLine($"Location: {request.Location}");
    Console.WriteLine($"Competitors: {request.Competitors}");
    Console.WriteLine($"Parking: {request.Parking}");
    Console.WriteLine($"Entrance: {request.Entrance}");
    Console.WriteLine($"Average Check: {request.AvgCheck}");
    Console.WriteLine($"Anchor: {request.Anchor}");
    Console.WriteLine($"Notes: {request.Notes}");
    Console.WriteLine($"Timestamp: {request.Timestamp}");
    Console.WriteLine("=============================\n");

    // Retornar string de respuesta
    return $"Análisis completado para {request.Customer} el {request.Timestamp}";
})
.WithName("AnalyzeCafe");

// Health check
app.MapGet("/health", () => "OK")
    .WithName("Health");

app.Run();

// Modelo para recibir datos del formulario

            /*Console.WriteLine("=".PadRight(70, '='));
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
            Console.WriteLine("Нажмите любую клавишу для выхода...");*/
            Console.ReadKey();
        }
        public class CafeRequest
{
    public string? Customer { get; set; }
    public string? Cuisine { get; set; }
    public string? Location { get; set; }
    public int Competitors { get; set; }
    public string? Parking { get; set; }
    public string? Entrance { get; set; }
    public int AvgCheck { get; set; }
    public string? Anchor { get; set; }
    public string? Notes { get; set; }
    public string? Timestamp { get; set; }
}
    }
}