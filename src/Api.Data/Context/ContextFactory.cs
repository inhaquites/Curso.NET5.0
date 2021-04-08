using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
  public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
  {
    public MyContext CreateDbContext(string[] args)
    {
      //Usado para criar as Migrations

      //conexao MySQL
      //var connectionString = "Server=localhost;Port=3306;Database=dbAPI;Uid=root;Pwd=So12022016";
      //var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
      //optionsBuilder.UseMySql(connectionString);
      // var optionsBuilder = new DbContextOptionsBuilder<MyContext>();

      // if (Environment.GetEnvironmentVariable("DATABASE").ToLower() == "SQLSERVER".ToLower())
      // {
      //conexao SQLServer Express
      var connectionString = "Server=.\\SQLEXPRESS;Initial Catalog=dbAPI2;Integrated Security=True";
      var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
      optionsBuilder.UseSqlServer(connectionString);
      // }
      // else
      // {
      //   //conexao MySQL
      //   var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
      //   optionsBuilder.UseMySql(connectionString);
      // }




      return new MyContext(optionsBuilder.Options);
    }
  }
}
