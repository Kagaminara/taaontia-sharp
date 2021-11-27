using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Text;
using TaaontiaCore.Database;


namespace TaaontiaLoader
{
    class Program
    {
        private const string FILE_NAME = "data.yml";

        static void CheckParam(string[] args)
        {
            if (!File.Exists(FILE_NAME))
            {
                Console.WriteLine($"The file {FILE_NAME} doesn't exist.");
                throw new Exception();
            }
        }

        static private void ShowResults(LoadResult results)
        {
            var sb = new StringBuilder();
            sb.AppendLine("-- Load completed --");
            if (results.LoadedStatuses > 0 || results.LoadedSkills > 0)
            {
                sb.AppendLine("Successfully loaded :");
                if (results.LoadedStatuses > 0)
                {
                    sb.AppendLine($"  {results.LoadedStatuses} statuses");
                }
                if (results.LoadedSkills > 0)
                {
                    sb.AppendLine($"  {results.LoadedSkills} skills");
                }
            }
            if (results.FailedStatuses > 0 || results.FailedSkills > 0)
            {
                sb.AppendLine("Failed to load :");
                if (results.FailedStatuses > 0)
                {
                    sb.AppendLine($"  {results.FailedStatuses} statuses");
                }
                if (results.FailedSkills > 0)
                {
                    sb.AppendLine($"  {results.FailedSkills} skills");
                }
            }
            Console.WriteLine(sb.ToString());
        }

        static void Main(string[] args)
        {
            CheckParam(args);
            var services = ConfigureServices();
            var results = services.GetRequiredService<TaaontiaLoader>().Load(FILE_NAME);
            ShowResults(results);
        }

        static private ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection()
                .AddSingleton<TaaontiaLoader>()
                .AddDbContext<TaaontiaEntities>();

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }

    }
}
