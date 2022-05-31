using System.ComponentModel.DataAnnotations;

namespace Discord_Bot.Database
{
    public class Event
    {
        public enum EEventType
        {
            // Stats changes
            HealthChange = 0,
            ManaChange,

            // Combat actions
            Engage,
            Attack,
            Defend,
            Flee,
        }

        [Key]
        public long Id { get; set; }
        public Fight Fight { get; set; }
        public EEventType Type { get; set; }
        public int Value { get; set; }
    }
}
