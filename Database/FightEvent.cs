using System.ComponentModel.DataAnnotations;

namespace Discord_Bot.Database
{
    public class FightEvent
    {
        public enum EFightEventType
        {
            Health = 0,
            Mana,
            Attack,
        }

        [Key]
        public long Id { get; set; }
        public Fight Fight { get; set; }
        public EFightEventType Type { get; set; }
        public int Value { get; set; }
    }
}
