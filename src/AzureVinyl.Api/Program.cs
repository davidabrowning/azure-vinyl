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
    List<Vinyl> vinyls = appDbContext.Vinyls.ToList();
    Vinyl extraVinyl = new Vinyl
    {
        Id = -1,
        Artist = "ABBA",
        Title = "Waterloo",
        Year = 1974
    };
    vinyls.Add(extraVinyl);
    return vinyls;
});

app.Run();
