using API.Configurations;
using API.Db.Context;
using API.Repositories.User;
using API.Services.Implementations;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Rewrite;
using API.Business;
using API.Business.Implementations;
using API.Repositories.Generic;
using API.Repositories.Client;
using API.Data.Converter.Implementations;
using API.Repositories.ServiceOrder;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var tokenConfigurations = new TokenConfiguration();

        new ConfigureFromConfigurationOptions<TokenConfiguration>(
            builder.Configuration.GetSection("TokenConfigurations")
            )
            .Configure(tokenConfigurations);

        builder.Services.AddSingleton(tokenConfigurations);

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = "Cookies";
        })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokenConfigurations.Issuer,
                    ValidAudience = tokenConfigurations.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.Secret))
                };
            })
            .AddMicrosoftAccount(microsoftOptions =>
            {
                microsoftOptions.ClientId = builder.Configuration["Authentication:Microsoft:ClientId"];
                microsoftOptions.ClientSecret = builder.Configuration["Authentication:Microsoft:ClientSecret"];
            })
            .AddCookie("Cookies");

        builder.Services.AddAuthorization(auth =>
        {
            auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build());

            auth.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
            auth.AddPolicy("Tecnichian", policy => policy.RequireRole("Tecnichian"));
        auth.AddPolicy("AdminOrTechnician", policy => policy.RequireAssertion(context =>
            context.User.IsInRole("Admin") || context.User.IsInRole("Tecnichian")));

            auth.AddPolicy("Client", policy => policy.RequireRole("Client"));
        });

        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        }));

        builder.Services.AddControllers();
        var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];
        builder.Services.AddDbContext<MySQLContext>(
            options => options.UseMySql(connection, ServerVersion.AutoDetect(connection))
            );


        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

        //Versioning API
        builder.Services.AddApiVersioning();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(
            "v1",
            new OpenApiInfo
            {
                Title = "Assitência Técnica",
                Version = "v1",
                Description = "API REST developed 'REST API' with ASP.NET Core 8",
                Contact = new OpenApiContact
                {
                    Name = "Daniel Sousa",
                    Url = new Uri("https://github.com/leandrocgsi")
                }
            });
        });

        // Dependency Injection
        builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        builder.Services.AddScoped<ILoginBusiness, LoginBusinessImplementation>();
        builder.Services.AddScoped<IUserBusiness, UserBusinessImplementation>();
        builder.Services.AddScoped<IRoleBusiness, RoleBusinessImplementations>();
        builder.Services.AddScoped<IClientBusiness, ClientBusinessImplementation>();
        builder.Services.AddScoped<IEquipmentBusiness, EquipmentBusinessImplementation>();
        builder.Services.AddScoped<IServiceOrderBusiness, ServiceOrderBusinessImplementation>();

        builder.Services.AddScoped<ClientConverter>();
        builder.Services.AddScoped<AddressConverter>();
        builder.Services.AddScoped<PartConverter>();
        builder.Services.AddScoped<ServiceOrderConverter>();
        builder.Services.AddScoped<EquipmentConverter>();


        builder.Services.AddTransient<ITokenService, TokenService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IClientRepository, ClientRepository>();
        builder.Services.AddScoped<IServiceOrderRepository, ServiceOrderRepository>();

        builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors();

        app.MapControllers();
        app.MapControllerRoute("DefaultApi", "{controller=values}/v{version=apiVersion}/{id?}");

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json",
                "API REST developed 'REST API' with ASP.NET Core 8");
        });

        var option = new RewriteOptions();
        option.AddRedirect("^$", "swagger");
        app.UseRewriter(option);

        app.Run();
    }
}