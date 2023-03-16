using Microsoft.EntityFrameworkCore;
using RestApi.DB;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

    app.MapGet("/", () => "Hello World!");

    app.Run();
    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthorization();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
    
builder.Services.AddDbContext<dbContext>(x=>
    x.UseNpgsql(builder.Configuration.GetConnectionString("connString")));
    
builder.Services.AddControllers();