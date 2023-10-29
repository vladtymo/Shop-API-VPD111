using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopAPI_vpd111.Middlewares;

var builder = WebApplication.CreateBuilder(args);

string connStr = builder.Configuration.GetConnectionString("LocalDb")!;

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<Shop111DbContext>(opts => opts.UseSqlServer(connStr));
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<Shop111DbContext>()
    .AddDefaultTokenProviders();

// configure custom services
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IAccountsService, AccountsService>();
// configure AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// add exception handling middleware
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
