
using KavehNegar.Logic.Repository.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Collections.Concurrent;
using KavehNegar.Logic.Context;
using KavehNegar.Logic.Model;
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

            var list = new ConcurrentBag<string>();
            string[] dirNames = { ".", ".." };
            List<Task> tasks = new List<Task>();
            foreach (var dirName in dirNames)
            {
                Task t = Task.Run(() =>
                {

                });
                tasks.Add(t);
            }
            Task.WaitAll(tasks.ToArray());
            foreach (Task t in tasks)
                Console.WriteLine("Task {0} Status: {1}", t.Id, t.Status);

            Console.WriteLine("Number of files read: {0}", list.Count);
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