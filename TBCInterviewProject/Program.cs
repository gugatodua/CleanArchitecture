using Application;
using Application.Persons;
using Application.Persons.Commands;
using FluentValidation;
using Persistence;
using Persistence.Repositories;
using TBCInterviewProject.Api.Middleware;
using TBCInterviewProject.Api.Validation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddTransient<IValidator<CreatePersonCommand>, CreatePersonCommandValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandler>();
app.UseMiddleware<Localizer>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();