using System.Reflection;
using Api.Mediator;
using Api.Utils;
using Data;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Services.Validators.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Library Api",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Daniel Londoño Ospina",
                        Email = "londonoospinadaniel@gmail.com"
                    }
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

Console.WriteLine("Adding DB");
var dbConnectionString = DataUtils.GetDbConnectionString(builder.Configuration, builder.Environment.ContentRootPath);
builder.Services.AddDbContext<Context>(options =>
                    options.UseSqlServer(dbConnectionString,
                    sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null);
                        sqlOptions.MigrationsAssembly("Api");
                    }));


Console.WriteLine("Adding AutoMapper");
builder.Services.AddAutoMapper(Assembly.Load("Services"));

Console.WriteLine("Adding MediatR");
builder.Services.AddMediatRConf();

Console.WriteLine("Adding DI");
builder.Services.AddScoped<IContext, Context>();
builder.Services.AddScoped<ICommonValidators, CommonValidators>();
builder.Services.AddValidatorsFromAssembly(typeof(CommonValidators).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
