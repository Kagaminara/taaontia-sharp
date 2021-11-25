namespace TaaontiaCore.Utils
{
    public interface ILoadedSkill
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public int BaseSourceDamage { get; set; }
        public int BaseTargetDamage { get; set; }
        public uint SourceStatus { get; set; }
        public uint TargetStatus { get; set; }

    }
}
