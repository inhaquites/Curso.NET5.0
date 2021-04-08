using System;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
  public abstract class BaseTest
  {
    public BaseTest()
    {

    }
  }

  public class DbTeste : IDisposable
  {
    private string dataBaseName = $"dbApiTest_{Guid.NewGuid().ToString().Replace("-", string.Empty)}";

    public ServiceProvider ServiceProvider { get; private set; }

    public DbTeste()
    {
      var serviceCollection = new ServiceCollection();
      serviceCollection.AddDbContext<MyContext>(c =>
        //LOCAL
        c.UseSqlServer($"Server=.\\SQLEXPRESS;Initial Catalog={dataBaseName};Integrated Security=True"),
        //NA WEB
        //c.UseSqlServer($"Data Source=198.38.83.200; Initial Catalog=ricardoz_solicitacao; User Id=ricardoz_solicitacao; Password=200461;"),
        //c.UseSqlServer($"Server=198.38.83.200; Initial Catalog={dataBaseName}; User Id=ricardoz_solicitacao; Password=200461;"),

        ServiceLifetime.Transient
      );

      ServiceProvider = serviceCollection.BuildServiceProvider();
      using (var Context = ServiceProvider.GetService<MyContext>())
      {
        Context.Database.EnsureCreated();
      }

    }

    public void Dispose()
    {
      using (var Context = ServiceProvider.GetService<MyContext>())
      {
        Context.Database.EnsureDeleted();
      }
    }
  }
}
