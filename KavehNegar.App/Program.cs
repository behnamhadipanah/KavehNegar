
using KavehNegar.Logic.Repository.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using KavehNegar.Logic.Context;
using KavehNegar.Logic.Model;
using KavehNegar.Logic.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace KavehNegar.App
{

    public class Program
    {
        protected static IConfigurationRoot Configuration { get; set; }

        static async Task Main(string[] args)
        {
            #region config

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false);
            using IHost host = CreateHostBuilder(args).Build();


            Configuration = builder.Build();


            #endregion


            var displayData = Task.Factory.StartNew(() => {
                var excel = new ExcelServices();
                return  excel.Read();
            }).
                ContinueWith((x) => {
                        var redis = new RedisService(host.Services);
                        var writeRedis = redis.Write(x.Result);
                }).
                ContinueWith((x) => {
                    var redis = new RedisService(host.Services);
                    var readRedis = redis.Read();

                    var sql = new SqlService(Configuration);
                    return sql.Write(readRedis);
                });
            Console.WriteLine(displayData.Result);

        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddScoped<IBaseRepository, BaseRepository>()
                        .AddScoped<IRedisContext, RedisContext>()
                        .Configure<RedisDBConfigs>(options => Configuration.GetSection("RedisDBConfigs").Bind(options))
                );
    }



}