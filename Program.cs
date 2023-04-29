using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProgramListWebAPI.Models;
using ProgramListWebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<ProgramListDatabaseSettings>(builder.Configuration.GetSection(nameof(ProgramListDatabaseSettings)));

builder.Services.AddSingleton<IProgramListDatabaseSettings>(sp => sp.GetRequiredService<IOptions<ProgramListDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(builder.Configuration.GetValue<string>("ProgramListDataBaseSettings:ConnectionString")));

builder.Services.AddScoped<IProgramListService, ProgramListService>();

builder.Services.AddScoped<ITopicListService, TopicListService>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//To solve cors error
builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
{
    build.WithOrigins("http://localhost:3000", "http://localhost:3001").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseSwagger();
app.UseSwaggerUI();

//To solve cors error
app.UseCors("corspolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
