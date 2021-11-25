using System;
using System.ComponentModel.DataAnnotations;

namespace TaaontiaCore.Database.Models
{
    /// <summary>
    /// Describe an Event.
    /// Every action in the game should create an Event,
    /// which will be used mainly for statistics purpose.
    /// </summary>
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
        public Guid Id { get; set; }
        public Fight Fight { get; set; }
        public Character SourceId { get; set; }
        public Character Target { get; set; }
        public EEventType Type { get; set; }
        public int Value { get; set; }
    }
}
