using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using TaaontiaCore.Database;
using TaaontiaCore.Services;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace TaaontiaCore.Utils
{
    public class SkillsAndStatusesLoader
    {
        private readonly TaaontiaEntities _db;
        private readonly LoggingService _logging;

        /// <summary>
        /// Load a yaml file
        /// </summary>
        /// <param name="path">Path to the file to load.</param>
        public SkillsAndStatusesLoader(IServiceProvider services)
        {

            _db = services.GetRequiredService<TaaontiaEntities>();
            _logging = services.GetRequiredService<LoggingService>();
        }

        /// <summary>
        /// Loads Skills from file. If any Status is triggered by
        /// a Skill, related Statuses should be loaded beforehand
        /// using LoadStatuses method.
        /// <see cref="LoadStatuses"/>
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool LoadSkills(string path)
        {
            try
            {
                string dataString = File.ReadAllText(path);
                var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
                var skills = deserializer.Deserialize<ILoadedSkill[]>(dataString);
                // Put it in DB

            }
            catch (Exception)
            {
                Console.WriteLine($"Could not read file at location: {path}");
                return false;
            }
            return true;
        }

        public bool LoadStatuses(string path)
        {
            try
            {
                string dataString = File.ReadAllText(path);
                var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
                var skills = deserializer.Deserialize<ILoadedStatus[]>(dataString);

            }
            catch (Exception)
            {
                Console.WriteLine($"Could not read file at location: {path}");
                return false;
            }
            return true;
        }
    }
}
