using Card.Type;

namespace Card
{
    public class FounderCard
    {
        public FounderCard(FounderCardType type, SuperPowerSkill skill, double money)
        {
            Type = type;
            Skill = skill;
            Money = money;
        }

        public FounderCardType Type { get; set; }

        public SuperPowerSkill Skill { get; set; }

        public double Money { get; set; }
    }
}
