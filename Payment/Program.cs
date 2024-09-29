using Application.Common;
using Application.Handlers;
using Application.Mappings;
using Application.Services;
using Application.Validators;
using Core.Events;
using Core.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.EventHandlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Reflection;
using System.Runtime.Loader;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PaymentDbContext>(options =>
   options.UseSqlServer(
       builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MappingProfile));

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CreateTransactionDtoValidator>();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();


//Event Handlers
builder.Services.AddScoped<DomainEventDispatcher>();
builder.Services.AddScoped<IEventHandler<TransactionCancelledEvent>, TransactionCancelledEventHandler>();
builder.Services.AddScoped<IEventHandler<TransactionSucceededEvent>, TransactionSucceededEventHandler>();
builder.Services.AddScoped<IEventHandler<TransactionRefundedEvent>, TransactionRefundedEventHandler>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBankService, BankService>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CancelTransactionCommandHandler).Assembly));

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();
app.MapControllers();
app.Run();