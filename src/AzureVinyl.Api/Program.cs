using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/api/vinyls", (AppDbContext appDbContext) =>
{
    return appDbContext.Vinyls.ToList();
});

app.MapPost("/api/vinyls", (AppDbContext appDbContext, Vinyl vinyl) =>
{
    vinyl.Id = 0;
    appDbContext.Vinyls.Add(vinyl);
    appDbContext.SaveChanges();
    return Results.Created($"/api/vinyls/{vinyl.Id}", vinyl);
});

app.Run();
