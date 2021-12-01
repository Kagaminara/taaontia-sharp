using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Text;
using TaaontiaCore.Database;


namespace TaaontiaLoader
{
    [Flags]
    public enum ELoadedType : short
    {
        None = 0,
        Status = 1,
        Skill = 2,
        FiendType = 4,
    }


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

        static private void ShowResults(LoadResult results, ELoadedType flags)
        {
            var sb = new StringBuilder();
            sb.AppendLine("-- Load completed --");
            sb.AppendLine("Successfully loaded :");
            if (flags.HasFlag(ELoadedType.Status)) sb.AppendLine($"  {results.LoadedStatuses} statuses");
            if (flags.HasFlag(ELoadedType.Skill)) sb.AppendLine($"  {results.LoadedSkills} skills");
            if (flags.HasFlag(ELoadedType.FiendType)) sb.AppendLine($"  {results.LoadedFiendTypes} fiend types");
            sb.AppendLine("Updated :");
            if (flags.HasFlag(ELoadedType.Status)) sb.AppendLine($"  {results.UpdatedStatuses} statuses");
            if (flags.HasFlag(ELoadedType.Status)) sb.AppendLine($"  {results.UpdatedSkills} skills");
            if (flags.HasFlag(ELoadedType.FiendType)) sb.AppendLine($"  {results.UpdatedFiendTypes} fiend types");
            sb.AppendLine("Failed to load :");
            if (flags.HasFlag(ELoadedType.Status)) sb.AppendLine($"  {results.FailedStatuses} statuses");
            if (flags.HasFlag(ELoadedType.Status)) sb.AppendLine($"  {results.FailedSkills} skills");
            if (flags.HasFlag(ELoadedType.FiendType)) sb.AppendLine($"  {results.FailedFiendTypes} fiend types");
            Console.WriteLine(sb.ToString());
        }

        static private ELoadedType GetFlags(string[] args)
        {
            ELoadedType flags = ELoadedType.None;
            if (args.Contains("--skills"))
            {
                flags |= ELoadedType.Skill;
            }
            if (args.Contains("--statuses"))
            {
                flags |= ELoadedType.Status;
            }
            if (args.Contains("--fiend-types"))
            {
                flags |= ELoadedType.FiendType;
            }
            return flags;
        }

        static void Main(string[] args)
        {
            CheckParam(args);
            var flags = GetFlags(args);
            if (flags == ELoadedType.None)
            {
                Console.WriteLine("You have to specify at least one model to load from the file.\nUsage: ./TaaontiaLoader.exe [--skills] [--statuses] [--fiend-types]");
                Environment.Exit(0);
            }
            var services = ConfigureServices();
            var results = services.GetRequiredService<TaaontiaLoader>().Load(FILE_NAME, flags);
            ShowResults(results, flags);
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
