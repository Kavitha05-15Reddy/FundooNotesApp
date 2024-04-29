using LogicLayer.Interface;
using LogicLayer.Service;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Web;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System.Text;

namespace FundooNotesApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var logpath = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
            NLog.GlobalDiagnosticsContext.Set("LogDirectory", logpath);
            builder.Logging.ClearProviders();
            builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            builder.Host.UseNLog();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<FundooContext>(opts => opts.UseSqlServer(builder.Configuration["ConnectionString:FundooDBp"]));

            builder.Services.AddStackExchangeRedisCache(options => { options.Configuration = builder.Configuration["RedisCacheUrl"]; });

            builder.Services.AddTransient<IUserRL,UserRL>();
            builder.Services.AddTransient<IUserBL, UserBL>();

            builder.Services.AddTransient<ICustomerRL,CustomerRL>();
            builder.Services.AddTransient<ICustomerBL, CustomerBL>();

            builder.Services.AddTransient<INotesRL,NotesRL>();
            builder.Services.AddTransient<INotesBL, NotesBL>();

            builder.Services.AddTransient<ILabelRL, LabelRL>();
            builder.Services.AddTransient<ILabelBL, LabelBL>();

            builder.Services.AddTransient<ICollaboratorRL, CollaboratorRL>();
            builder.Services.AddTransient<ICollaboratorBL, CollaboratorBL>();

            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.SameSite = SameSiteMode.None; // or SameSiteMode.Lax or SameSiteMode.Strict
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });
            builder.Services.AddDistributedMemoryCache();


            builder.Services.AddMassTransit(x =>
            {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.UseHealthCheck(provider);
                    config.Host(new Uri("rabbitmq://localhost"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                }));
            });
            builder.Services.AddMassTransitHostedService();
            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseSession();

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseAuthorization();       

            app.MapControllers();

            app.Run();
        }
    }
}
