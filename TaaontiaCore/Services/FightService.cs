using Microsoft.Extensions.DependencyInjection;
using System;
using TaaontiaCore.Database;
using TaaontiaCore.Events;

namespace TaaontiaCore.Services
{
    public class FightService
    {
        private readonly TaaontiaEntities _db;
        private readonly LoggingService _logging;

        public FightService(IServiceProvider services)
        {
            _db = services.GetRequiredService<TaaontiaEntities>();
            _logging = services.GetRequiredService<LoggingService>();
        }

        public FightResult Engage()
        {
            return new FightResult();
        }

        public FightResult Flee()
        {
            return new FightResult();
        }

        public FightResult Action(FightEvent e)
        {
            return new FightResult();
        }
    }
}
