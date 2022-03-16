using Autofac;
using Autofac.Extensions.DependencyInjection;
using Sword.Application;
using Sword.Core;
using Sword.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Serilog;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;

namespace Sword.Api
{
    public class Program
    {
        public static string Audience = "https://api.sword.com";
        public static string Issuer = "https://api.sword.com";
        public static string Secret = "Wjagjbmui5ZBCC0nV6HMdTsEYjznXJGqhOVIRbH50P8=";
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(budr =>
                {
                    budr.ConfigureServices((cntx, svcs) =>
                    {
                        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
                        svcs.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(opts =>
                            {
                                opts.TokenValidationParameters = new TokenValidationParameters
                                {
                                    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(Program.Secret)),
                                    RequireExpirationTime = true,
                                    ValidAudience = Program.Audience,
                                    ValidIssuer = Program.Issuer,
                                    ValidateAudience = true,
                                    ValidateIssuer = true,
                                    ValidateIssuerSigningKey = true,
                                    ValidateLifetime = true
                                };
                            });
                        svcs.AddAutoMapper(typeof(SwordProfile).Assembly);
                        svcs.AddControllers(opts =>
                        {
                            opts.Filters.Add(typeof(ActionFilter));
                            opts.Filters.Add(typeof(ExceptionFilter));
                        }).AddNewtonsoftJson(opts =>
                        {
                            opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                        });
                        svcs.AddCors(opts =>
                        {
                            opts.AddDefaultPolicy(budr =>
                            {
                                budr.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
                                    .WithExposedHeaders("X-Entity", "X-Permission", "X-Token", "X-Validation", "X-Version");
                            });
                        });
                        svcs.AddDbContext<SwordDbContext>(budr =>
                        {
                            budr.UseSqlServer(cntx.Configuration.GetConnectionString("DefaultConnection"));
                        });
                        svcs.AddHttpContextAccessor();
                        svcs.AddSwaggerGen(opts =>
                        {
                            opts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                            {
                                Description = "Please enter into field the word 'Bearer' followed by a space and the JWT value",
                                Name = "Authorization",
                                In = ParameterLocation.Header,
                                Type = SecuritySchemeType.ApiKey
                            });
                            opts.AddSecurityRequirement(new OpenApiSecurityRequirement
                            {
                                {
                                    new OpenApiSecurityScheme
                                    {
                                        Reference = new OpenApiReference
                                        {
                                            Id = "Bearer",
                                            Type = ReferenceType.SecurityScheme
                                        }
                                    }, Array.Empty<string>()
                                }
                            });
                            opts.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "appdocument.xml"), true);
                            opts.SwaggerDoc("v1", new OpenApiInfo
                            {
                                Title = "Sword.Api Title",
                                Version = "v1",
                                Description = "Sword.Api Description",
                                Contact = new OpenApiContact
                                {
                                    Name = "Sword",
                                    Email = "sword@hotmail.com"
                                }
                            });
                        });
                    });

                    budr.Configure((cntx, budr) =>
                    {
                        budr.UseSerilogRequestLogging();
                        //budr.UseDefaultFiles();
                        budr.UseStaticFiles();
                        budr.UseRouting();
                        budr.UseCors();
                        budr.UseAuthentication();
                        budr.UseAuthorization();
                        budr.UseEndpoints(x => x.MapControllers());
                        budr.UseSwagger(x => x.RouteTemplate = "swagger/{documentName}/swagger.json");
                        budr.UseSwaggerUI(x => x.SwaggerEndpoint("v1/swagger.json", "The Sword API"));
                    });
                })
                .UseSerilog((cntx, logr) => logr.ReadFrom.Configuration(cntx.Configuration))
                .UseServiceProviderFactory(cntx => new AutofacServiceProviderFactory(budr =>
                {
                    budr.RegisterType<SwordDbContext>()
                        .As<ITransaction>()
                        .InstancePerLifetimeScope();
                    budr.RegisterAssemblyTypes(typeof(SwordDbContext).Assembly)
                        .AsClosedTypesOf(typeof(IRepository<>))
                        .InstancePerLifetimeScope();
                }));
    }
}
