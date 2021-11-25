using Microsoft.Extensions.DependencyInjection;
using System;
using TaaontiaCore.Database;
using TaaontiaCore.Events.Fight;

namespace TaaontiaCore.Services
{
    public class GameService
    {
        private readonly TaaontiaEntities _db;
        private readonly LoggingService _logging;

        public GameService(IServiceProvider services)
        {
            _db = services.GetRequiredService<TaaontiaEntities>();
            _logging = services.GetRequiredService<LoggingService>();
        }

        public FightResult HandleFight(FightEvent fightEvent)
        {
            return new FightResult
            {
                Result = EFightResult.SUCCESS,

            };
        }

        public void SayBye()
        {
            _logging.Log("poeut");
        }
    }
}
