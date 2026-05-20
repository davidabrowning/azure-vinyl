var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/api/vinyls", () =>
{
    return "Hello World!";
});

app.Run();
