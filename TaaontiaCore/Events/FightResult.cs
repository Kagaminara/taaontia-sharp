using TaaontiaCore.Database.Models;
using TaaontiaCore.DTO;
using TaaontiaCore.Enums;

namespace TaaontiaCore.Events
{
    public class FightResult : ResultBase
    {
        public EFightError? Error;
        public Fight Fight;
        public int? SourceDamage;
        public int? TargetDamage;
        public Status SourceStatus;
        public Status TargetStatus;
        public FightCompleteRewards Rewards;
    }
}
