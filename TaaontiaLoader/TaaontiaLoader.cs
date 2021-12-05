using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text;
using TaaontiaCore.Database;
using TaaontiaCore.Database.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace TaaontiaLoader
{
    public class LoadResult
    {
        public uint LoadedSkills { get; set; } = 0;
        public uint UpdatedSkills { get; set; } = 0;
        public uint FailedSkills { get; set; } = 0;
        public uint LoadedStatuses { get; set; } = 0;
        public uint UpdatedStatuses { get; set; } = 0;
        public uint FailedStatuses { get; set; } = 0;
        public uint LoadedFiendTypes { get; set; } = 0;
        public uint UpdatedFiendTypes { get; set; } = 0;
        public uint FailedFiendTypes { get; set; } = 0;
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

        public LoadResult Load(string path, ELoadedType flags)
        {
            using (var dataString = File.OpenText(path))
            {
                var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
                var data = deserializer.Deserialize<LoadedData>(dataString);

                LoadResult result = new LoadResult();
                var sb = new StringBuilder();
                if (flags.HasFlag(ELoadedType.Status))
                {
                    // Check Statuses Ids
                    if (data.Statuses == null)
                    {
                        sb.AppendLine($"--statuses has been provided but no statuses could be found.");
                    }
                    else
                    {
                        foreach (var status in data.Statuses)
                        {
                            if (status.Id == null)
                            {
                                sb.AppendLine($"Could not load Status {status.Name} because it has no Id");
                                result.FailedStatuses++;
                                continue;
                            }

                            var existingStatusType = _db.StatusType.FirstOrDefault(type => type.Id == status.Id);
                            var isUpdate = true;
                            if (existingStatusType == null)
                            {
                                existingStatusType = new StatusType();
                                isUpdate = false;
                            }

                            existingStatusType.Id = (uint)status.Id;
                            existingStatusType.BaseValue = status.BaseValue;
                            existingStatusType.Description = status.Description;
                            existingStatusType.Duration = status.Duration;
                            existingStatusType.Effect = status.Effect;
                            existingStatusType.Name = status.Name;

                            try
                            {
                                if (isUpdate)
                                {
                                    _db.StatusType.Update(existingStatusType);
                                    result.UpdatedStatuses++;
                                }
                                else
                                {
                                    _db.StatusType.Add(existingStatusType);
                                    result.LoadedStatuses++;
                                }

                            }
                            catch (Exception)
                            {
                                Console.WriteLine($"Failed to upsert status with id {status.Id}.");
                                result.FailedStatuses++;
                                continue;
                            }
                        }
                    }
                }

                if (flags.HasFlag(ELoadedType.Skill))
                {
                    // Check Skill's statuses
                    if (data.Skills == null)
                    {
                        sb.AppendLine($"--skills has been provided but no skills could be found.");
                    }
                    else
                    {
                        foreach (var skill in data.Skills)
                        {
                            if (skill.Id == null)
                            {
                                sb.AppendLine($"Could not load Skill {skill.Name} because it has no Id");
                                result.FailedSkills++;
                                continue;
                            }
                            else
                            {
                                if (skill.SourceStatus != null && !_db.StatusType.Any(status => status.Id == skill.SourceStatus))
                                {
                                    sb.AppendLine($"Could not find SourceStatus with id '{skill.SourceStatus}' for skill '{skill.Name}'");
                                    result.FailedSkills++;
                                    continue;
                                }
                                if (skill.TargetStatus != null && !_db.StatusType.Any(status => status.Id == skill.TargetStatus))
                                {
                                    sb.AppendLine($"Could not find TargetStatus with id '{skill.SourceStatus}' for skill '{skill.Name}'");
                                    result.FailedSkills++;
                                    continue;
                                }
                            }

                            var existingSkill = _db.Skill.FirstOrDefault(type => type.Id == skill.Id);
                            var isUpdate = true;
                            if (existingSkill == null)
                            {
                                existingSkill = new Skill();
                                isUpdate = false;
                            }

                            existingSkill.Id = (uint)skill.Id;
                            existingSkill.BaseSourceDamage = skill.BaseSourceDamage;
                            existingSkill.BaseTargetDamage = skill.BaseTargetDamage;
                            existingSkill.Description = skill.Description;
                            existingSkill.Name = skill.Name;

                            try
                            {
                                StatusType sourceStatus = null;
                                StatusType targetStatus = null;
                                if (skill.SourceStatus != null)
                                {
                                    sourceStatus = _db.StatusType.Find(skill.SourceStatus);
                                    existingSkill.SourceStatus = sourceStatus;
                                }
                                if (skill.TargetStatus != null)
                                {
                                    targetStatus = _db.StatusType.Find(skill.TargetStatus);
                                    existingSkill.TargetStatus = targetStatus;
                                }

                                if (isUpdate)
                                {
                                    _db.Skill.Update(existingSkill);
                                    result.UpdatedSkills++;
                                }
                                else
                                {
                                    _db.Skill.Add(existingSkill);
                                    result.LoadedSkills++;
                                }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine($"Failed to upsert skill with id {skill.Id}.");
                                result.FailedSkills++;
                                continue;
                            }
                        }
                    }
                }

                if (flags.HasFlag(ELoadedType.FiendType))
                {
                    // Check Skill's statuses
                    if (data.FiendTypes == null)
                    {
                        sb.AppendLine($"--fiend-types has been provided but no fiend types could be found.");
                    }
                    else
                    {
                        foreach (var fiendType in data.FiendTypes)
                        {
                            if (fiendType.Name == null)
                            {
                                sb.AppendLine($"Fiend type with Id {fiendType.Id} has no name.");
                                result.FailedFiendTypes++;
                                continue;
                            }

                            var existingFiendType = _db.FiendType.FirstOrDefault(type => type.Id == fiendType.Id);
                            var isUpdate = true;
                            if (existingFiendType == null)
                            {
                                existingFiendType = new FiendType();
                                isUpdate = false;
                            }

                            existingFiendType.Id = fiendType.Id;
                            existingFiendType.Name = fiendType.Name;
                            existingFiendType.Description = fiendType.Description;
                            existingFiendType.BaseEnergy = fiendType.BaseEnergy;
                            existingFiendType.BaseHealth = fiendType.BaseHealth;
                            existingFiendType.Skills = _db.Skill.Where(skill => fiendType.Skills.Contains(skill.Id)).ToList();

                            try
                            {
                                if (isUpdate)
                                {
                                    _db.FiendType.Update(existingFiendType);
                                    result.UpdatedFiendTypes++;
                                }
                                else
                                {
                                    _db.FiendType.Add(existingFiendType);
                                    result.LoadedFiendTypes++;
                                }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine($"Failed to upsert fiend type with id {fiendType.Id}.");
                                result.FailedFiendTypes++;
                                continue;
                            }
                        }
                    }
                }

                _db.SaveChanges();
                Console.WriteLine(sb.ToString());
                return result;
            }
        }
    }
}
