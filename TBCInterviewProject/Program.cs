using Application;
using Application.Behaviours;
using Application.Persons;
using Application.Persons.Commands;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Persistence;
using Persistence.Repositories;
using TBCInterviewProject.Api;
using TBCInterviewProject.Api.Middleware;
using TBCInterviewProject.Api.Resources;
using TBCInterviewProject.Api.Validation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TbcDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("TbcDatabase")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IStringLocalizer, StringLocalizer<ErrorResources>>();

builder.Services.AddValidatorsFromAssemblyContaining<CreatePersonCommandValidator>();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddMediatR(typeof(CreatePersonCommandHandler).Assembly);

builder.Services.AddAutoMapper(typeof(PersonMappingProfile));

var imageSettings = builder.Configuration.GetSection("ImageSettings").Get<ImageSettings>();
ImageValidator.Initialize(imageSettings.AllowedExtensions);

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