using System.Reflection;
using System.Text;
using FeatureToggle.Application.Requests.Commands.UserCommands;
using FeatureToggle.Domain.ConfigurationModels;
using FeatureToggle.Domain.Entity.FeatureManagementSchema;
using FeatureToggle.Infrastructure.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddIdentityApiEndpoints<User>()
                .AddEntityFrameworkStores<FeatureManagementContext>();
///below used addidentity earlier
builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<FeatureManagementContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<Authentication>(builder.Configuration.GetSection("Authentication"));
//builder.Services.AddTransient<UserValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AddUserCommandValidator>();



builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789.@";
    options.User.RequireUniqueEmail = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireDigit = false;
});

builder.Services.AddAuthentication(x =>
                                    {
                                        x.DefaultAuthenticateScheme =
                                        x.DefaultChallengeScheme =
                                        x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                                    })
                                    .AddJwtBearer(x =>
                                    {
                                        x.SaveToken = false;
                                        x.TokenValidationParameters = new TokenValidationParameters
                                        {
                                            ValidateIssuerSigningKey = true,
                                            IssuerSigningKey = new SymmetricSecurityKey(
                                                Encoding.UTF8.GetBytes(builder.Configuration["Authentication:JWTSecret"]!))
                                        };
                                    });

builder.Services.AddDbContext<FeatureManagementContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("FeatureManagementDbContext")));
builder.Services.AddDbContext<BusinessContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("FeatureManagementDbContext")));

builder.Services.AddMediatR(x =>
    x.RegisterServicesFromAssembly(Assembly.Load("FeatureToggle.Application"))
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
