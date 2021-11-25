﻿using Microsoft.Extensions.DependencyInjection;
using System;
using TaaontiaCore.Database;

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

        public void SayBye()
        {
            _logging.Log("poeut");
        }
    }
}