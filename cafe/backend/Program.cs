using FuzzyRestaurantEvaluation.Models;
var builder = WebApplication.CreateBuilder(args);
MaximRules mr=new MaximRules();
// Agregar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173", "http://localhost:3000", "https://joantarazona99.github.io", "https://389d14fd.cafe-133.pages.dev", "https://cafe-133.pages.dev")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Servir archivos estáticos del frontend (wwwroot/)
app.UseDefaultFiles();
app.UseStaticFiles();

// Usar CORS
app.UseCors("AllowFrontend");

// Endpoint para recibir datos del formulario
app.MapPost("/api/cafe/analyze", (CafeRequest request,HttpContext context) =>
{
    // Imprimir datos en consola
    Console.WriteLine("=== ЗАПРОС НА АНАЛИЗ КАФЕ ===");
    Console.WriteLine($"Клиент: {request.Customer}");
    Console.WriteLine($"Кухня: {request.Cuisine}");
    Console.WriteLine($"Расположение: {request.Location}");
    Console.WriteLine($"Конкуренты: {request.Competitors}");
    Console.WriteLine($"Парковка: {request.Parking}");
    Console.WriteLine($"Вход: {request.Entrance}");
    Console.WriteLine($"Средний чек: {request.AvgCheck}");
    Console.WriteLine($"Якорь: {request.Anchor}");
    Console.WriteLine($"Заметки: {request.Notes}");
    Console.WriteLine($"Дата и время: {request.Timestamp}");
    Console.WriteLine("=============================\n");
 string []D={"High","Medium","Low"};//Конкуренты
 string[]E={"Paid","None","Free"};//Парковка
 string[]F={"Through the arch","Through the building","Through the mall","From the street"};//тип входа

    // Retornar string de respuesta
    int d=Math.Min(2,request.Competitors);
    int e=Math.Max(0,Array.IndexOf(E,request.Parking));
    int f=Math.Max(0,Array.IndexOf(F,request.Entrance));
    //Array.IndexOf(D,)
    //Evaluate(double dCode, double eCode, double fCode)
    var res_=mr.Evaluate(d,e,f);
    Console.WriteLine(res_);

string[]R=new string[]{"Предполагаемый рейтинг меньше 2.51","Предполагаемый рейтинг от 2.51 до 4.51","Предполагаемый рейтинг выше 4.51"};
    //cooke
    var options = new CookieOptions
    {
        HttpOnly =false,
        Secure = false,
        SameSite = SameSiteMode.Lax,
        Expires = DateTimeOffset.UtcNow.AddSeconds(30*3)
    };
    context.Response.Cookies.Append("UserResult",$"Ваш предполагаемый рейтинг от {res_} до {res_+1} звезд " , options);

    return $"Анализ завершён для {request.Customer} — {request.Timestamp}";
})
.WithName("AnalyzeCafe");
//dotnet run CafeBackend.csproj
// Health check
app.MapGet("/health", () => "OK")
    .WithName("Health");

app.Run();

// Modelo para recibir datos del formulario
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
