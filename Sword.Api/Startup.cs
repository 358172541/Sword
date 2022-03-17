using Api.Filters;
using Application;
using EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Senparc.CO2NET;
using Senparc.CO2NET.AspNet;
using Senparc.Weixin;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.MessageHandlers.Middleware;
using Senparc.Weixin.RegisterServices;
using Serilog;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;

namespace Api
{
    public class Startup
    {
        public Startup(
            IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }
        public void ConfigureServices(IServiceCollection svcs)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            svcs.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opts =>
                {
                    opts.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(Configuration["AuthSetting:Secret"])),
                        RequireExpirationTime = true,
                        ValidAudience = Configuration["AuthSetting:Audience"],
                        ValidIssuer = Configuration["AuthSetting:Issuer"],
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true
                    };
                });
            svcs.AddAutoMapper(typeof(MapperProfile).Assembly);
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
            svcs.AddDbContext<AppDbContext>(budr =>
                {
                    budr.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                });
            svcs.AddHttpContextAccessor();

            //svcs.AddMemoryCache().AddSenparcWeixinServices(Configuration); // Senparc

            if (Environment.IsDevelopment())
            {
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
                        Title = "Api Title",
                        Version = "v1",
                        Description = "Api Description",
                        Contact = new OpenApiContact
                        {
                            Name = "Api",
                            Email = "api@hotmail.com"
                        }
                    });
                });
            }
        }

        public void Configure(
            IApplicationBuilder budr,
            IOptions<SenparcSetting> senparcSetting,
            IOptions<SenparcWeixinSetting> senparcWeixinSetting)
        {
            budr.UseSerilogRequestLogging();
            // budr.UseDefaultFiles();
            budr.UseStaticFiles();

            //budr.UseSenparcGlobal(Environment, senparcSetting.Value, _ => { }, true)
            //    .UseSenparcWeixin(senparcWeixinSetting.Value, (regr, sett) => regr.RegisterMpAccount(senparcWeixinSetting.Value)); // Senparc

            budr.UseRouting();
            budr.UseCors();
            budr.UseAuthentication();
            budr.UseAuthorization();

            //budr.UseMessageHandlerForMp("/wx", 
            //    (strm, post, cont, prdr) => new TestMessageHandler(strm, post, cont, prdr),
            //    opts => opts.AccountSettingFunc = context => senparcWeixinSetting.Value); // Senparc

            budr.UseEndpoints(x => x.MapControllers());

            if (Environment.IsDevelopment())
            {
                budr.UseSwagger(x => x.RouteTemplate = "swagger/{documentName}/swagger.json");
                budr.UseSwaggerUI(x => x.SwaggerEndpoint("v1/swagger.json", "The Sword API"));
            }
        }
    }
}
