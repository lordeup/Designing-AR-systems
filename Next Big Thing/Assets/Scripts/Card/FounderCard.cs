using Card.Type;

namespace Card
{
    public class FounderCard
    {
        public FounderCardType Type;
        public SuperPowerSkill Skill;
        public double Money;

        public FounderCard(FounderCardType type, SuperPowerSkill skill, double money)
        {
            Type = type;
            Skill = skill;
            Money = money;
        }
    }
}
