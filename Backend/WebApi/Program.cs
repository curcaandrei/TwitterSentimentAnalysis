using System.Web.Http;
using System.Web.Http.Cors;
using Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence;
using Persistence.MongoDb;
using Persistence.TwitterExternalAPI;
using Microsoft.Owin.Hosting;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.Configure<MongoSettings>(options =>
{
    options.Connection = builder.Configuration.GetSection("MongoSettings:Connection").Value;
    options.DatabaseName = builder.Configuration.GetSection("MongoSettings:DatabaseName").Value;
    
});
builder.Services.AddScoped<IMongoDbContext, MongoDbContext>();

// Twitter Helper
builder.Services.Configure<TwitterSettings>(options =>
{
    options.apiKey = builder.Configuration.GetSection("TwitterAPI:TwitterApiKey").Value;
    options.apiSecret = builder.Configuration.GetSection("TwitterAPI:TwitterApiSecret").Value;
    options.accessToken = builder.Configuration.GetSection("TwitterAPI:TwitterAccessToken").Value;
    options.accessSecret = builder.Configuration.GetSection("TwitterAPI:TwitterAccessTokenSecret").Value;
});
// builder.Services.AddAuthentication().AddTwitter(twitterOptions =>
// {
//     twitterOptions.ConsumerKey = builder.Configuration["TwitterAPI:TwitterApiKey"];
//     twitterOptions.ConsumerSecret = builder.Configuration["TwitterAPI:TwitterApiSecret"];
//     twitterOptions.RetrieveUserDetails = true;
// });

builder.Services.AddScoped<ITwitterHelper, TwitterHelper>();
// Twitter Helper
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
});
var app = builder.Build();

app.UseRouting();
app.UseCors(x =>
{
    x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(origin => true);
});
app.UseAuthorization();
app.UseEndpoints(x => x.MapControllers());
// Configure the HTTlP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DefaultModelsExpandDepth(-1);
        
    });
}

app.UseCors();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();