using TaaontiaCore.Database.Models;
using TaaontiaCore.Enums;

namespace TaaontiaCore.Events
{
    public class FightResult : ResultBase
    {
        public EFightError? Error;
        public Fight Fight;
        public int? SourceDamage;
        public int? TargerDamage;
        public Status SourceStatus;
        public Status TargetStatus;

    }
}
