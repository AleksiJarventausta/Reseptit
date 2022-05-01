using MongoDB.Driver;
using Reseptit;
using Reseptit.Models;
using Reseptit.Repositories;

var builder = WebApplication.CreateBuilder(args);


var services = builder.Services;
// Add services to the container.

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();

services.AddSingleton(sp => new MongoDbSettings()
{
    ConnectionString = builder.Configuration["ConnectionStrings:MongoDB"],
    DatabaseName = builder.Configuration["MongoDB:DatabaseName"]
});

services.AddSingleton(sp => new MongoClient(sp.GetService<MongoDbSettings>()?.ConnectionString));
services.AddScoped<IRepository<Recipe>,RecipeRepository>();

services.AddSwaggerGen();


var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
   // app.UseHsts();
}
else
{
    app.UseHttpsRedirection();
}

//app.UseStaticFiles();
//app.UseRouting();


app.MapControllers();

//Eagerly initialize MongoClient (otherwise would be lazy singleton). This helps with "Connection was paused" exception
app.Services.GetService<MongoClient>();

app.Run();