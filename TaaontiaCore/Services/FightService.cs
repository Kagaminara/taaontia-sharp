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

        }

        public FightResult Flee()
        {

        }

        public FightResult Action(FightEvent e)
        {

        }
    }
}
