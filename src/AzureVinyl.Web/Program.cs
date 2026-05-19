using System.Security.AccessControl;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    var connection = new SqlConnection(connectionString);
    connection.AccessToken = new DefaultAzureCredential()
        .GetToken(
            new AuthorizationRule.Core.TokenRequestContext(
                new[] { "https://database.windows.net/.default" }
            )
        ).Token;
        
    options.UseSqlServer(connection);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
