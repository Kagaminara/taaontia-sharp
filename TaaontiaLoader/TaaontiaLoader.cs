using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TaaontiaCore.Database;
using TaaontiaCore.Database.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace TaaontiaLoader
{
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public float HeightInInches { get; set; }
        public Dictionary<string, Address> Addresses { get; set; }

    }

    public class LoadResult
    {
        public uint LoadedSkills { get; set; } = 0;
        public uint FailedSkills { get; set; } = 0;
        public uint LoadedStatuses { get; set; } = 0;
        public uint FailedStatuses { get; set; } = 0;
    }

    public class TaaontiaLoader
    {
        private readonly TaaontiaEntities _db;

        /// <summary>
        /// Load a yaml file
        /// </summary>
        /// <param name="path">Path to the file to load.</param>
        public TaaontiaLoader(IServiceProvider services)
        {
            _db = services.GetRequiredService<TaaontiaEntities>();
        }

        private void IntegrityCheck(ref LoadedData data, ref LoadResult result)
        {
            var sb = new StringBuilder();
            var skills = new List<LoadedSkill>(data.Skills);
            var statuses = new List<LoadedStatus>(data.Statuses);
            // Check Statuses Ids
            foreach (var status in statuses)
            {
                if (status.Id == null)
                {
                    sb.AppendLine($"Could not load Status {status.Name} because it has no Id");
                    data.Statuses.Remove(status);
                }
            }

            // Check Skill's statuses
            foreach (var skill in skills)
            {
                bool error = false;
                if (skill.Id == null)
                {
                    sb.AppendLine($"Could not load Skill {skill.Name} because it has no Id");
                    error = true;
                }
                else
                {
                    if (skill.SourceStatus != null && !data.Statuses.Any(status => status.Id == skill.SourceStatus))
                    {
                        sb.AppendLine($"Could not find SourceStatus with id '{skill.SourceStatus}' for skill '{skill.Name}'");
                        error = true;
                    }
                    if (skill.TargetStatus != null && !data.Statuses.Any(status => status.Id == skill.TargetStatus))
                    {
                        sb.AppendLine($"Could not find TargetStatus with id '{skill.SourceStatus}' for skill '{skill.Name}'");
                        error = true;
                    }
                }
                if (error)
                {
                    data.Skills.Remove(skill);
                    result.FailedSkills += 1;
                }

            }
            Console.WriteLine(sb.ToString());
        }

        private void DatabaseUpdate(ref LoadedData data, ref LoadResult result)
        {
            _db.StatusType.AddRange(data.Statuses.Select(status => new StatusType
            {
                BaseValue = status.BaseValue,
                Description = status.Description,
                Duration = status.Duration,
                Effect = status.Effect,
                Id = (uint)status.Id,
                Name = status.Name,
            }));
            _db.Skill.AddRange(data.Skills.Select(skill =>
            {
                StatusType sourceStatus = null;
                StatusType targetStatus = null;
                if (skill.SourceStatus != null)
                {
                    sourceStatus = _db.StatusType.Find(skill.SourceStatus);
                }
                if (skill.TargetStatus != null)
                {
                    sourceStatus = _db.StatusType.Find(skill.TargetStatus);
                }

                return new Skill
                {
                    BaseSourceDamage = skill.BaseSourceDamage,
                    BaseTargetDamage = skill.BaseTargetDamage,
                    Description = skill.Description,
                    Id = (uint)skill.Id,
                    Name = skill.Name,
                    SourceStatus = sourceStatus,
                    TargetStatus = targetStatus,
                };
            }
            ));
            _db.SaveChanges();
        }

        public LoadResult Load(string path)
        {
            using (var dataString = File.OpenText(path))
            {
                var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
                var data = deserializer.Deserialize<LoadedData>(dataString);

                LoadResult result = new LoadResult();
                IntegrityCheck(ref data, ref result);
                result.LoadedSkills = (uint)data.Skills.Count;
                result.LoadedStatuses = (uint)data.Statuses.Count;
                DatabaseUpdate(ref data, ref result);
                return result;
            }
        }
    }
}
