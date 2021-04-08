using Api.CrossCutting.DependencyIngection;
using Api.CrossCutting.Mappings;
using Api.Data.Context;
using Api.Domain.Security;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace application
{
  public class Startup
  {
    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
      Configuration = configuration;
      _environment = environment;
    }

    public IConfiguration Configuration { get; }
    public IWebHostEnvironment _environment { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      if (_environment.IsEnvironment("Testing"))
      {
        Environment.SetEnvironmentVariable("DB_CONNECTION", "Server=.\\SQLEXPRESS;Initial Catalog=dbAPI_Integration;Integrated Security=True");
        Environment.SetEnvironmentVariable("DATABASE", "SQLSERVER");
        Environment.SetEnvironmentVariable("MIGRATION", "APLICAR");
        Environment.SetEnvironmentVariable("Audience", "ExemploAudience");
        Environment.SetEnvironmentVariable("Issuer", "ExemploIssuer");
        Environment.SetEnvironmentVariable("Seconds", "3600");
      }
      services.AddControllers();

      ConfigureService.ConfigureDependenciesService(services);
      ConfigureRepository.ConfigureDependenciesRepository(services);
      var config = new AutoMapper.MapperConfiguration(cfg =>
      {
        cfg.AddProfile(new DtoToModelProfile());
        cfg.AddProfile(new EntityToDtoProfile());
        cfg.AddProfile(new ModelToEntityProfile());
      });

      IMapper mapper = config.CreateMapper();
      services.AddSingleton(mapper);

      var signingConfigurations = new SigningConfigurations();
      services.AddSingleton(signingConfigurations);

      services.AddAuthentication(authOptions =>
      {
        authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(bearerOptions =>
      {
        var paramsValidation = bearerOptions.TokenValidationParameters;
        paramsValidation.IssuerSigningKey = signingConfigurations.Key;
        paramsValidation.ValidAudience = Environment.GetEnvironmentVariable("Audience");
        paramsValidation.ValidIssuer = Environment.GetEnvironmentVariable("Issuer");

        //valida a assinatura de um token recebido
        paramsValidation.ValidateIssuerSigningKey = true;

        //verifica se um token recebido ainda é valido
        paramsValidation.ValidateLifetime = true;

        //temo de tolerancia para a expiracao de um token (utilizado
        // caso haja problemas de sincronismo de horario entre diferentes
        // computadores envolvidos no processo de comunicacao)
        paramsValidation.ClockSkew = TimeSpan.Zero;
      });

      // Ativa o uso do token como forma de autorizar o acesso
      // a recursos deste projeto
      services.AddAuthorization(auth =>
      {
        auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                  .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                  .RequireAuthenticatedUser().Build());
      });

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
          Version = "v1",
          Title = "Curso de API com AspNetCore 3.1 - Na Prática",
          Description = "Arquitetura DDD",
          TermsOfService = new Uri("http://www.inhaquites.com.br"),
          Contact = new OpenApiContact
          {
            Name = "Rodrigo Inhaquites",
            Email = "r.inhaquites@gmail.com",
            Url = new Uri("http://www.inhaquites.com.br")
          },
          License = new OpenApiLicense
          {
            Name = "Termo de licença de uso",
            Url = new Uri("http://www.inhaquites.com.br")
          }
        });

        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
          Description = "Informe o Token JWT",
          Name = "Authorization",
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.ApiKey
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
          {
            new OpenApiSecurityScheme {
              Reference = new OpenApiReference {
                  Id = "Bearer",
                  Type = ReferenceType.SecurityScheme
              }
            }, new List<string>()
          }
        });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Curso de API com AspNetCore 3.1");
        c.RoutePrefix = string.Empty;
      });

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      //rodar migrations na primeira vez que execura o sistema
      if (Environment.GetEnvironmentVariable("MIGRATION").ToLower() == "APLICAR".ToLower())
      {
        using (var service = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                                                    .CreateScope())
        {
          using (var context = service.ServiceProvider.GetService<MyContext>())
          {
            context.Database.Migrate();
          }
        }
      }
    }
  }
}
