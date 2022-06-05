using Card.Type;

namespace Card
{
    public class CompanyCard
    {
        public CompanyCardType Type;
        public double Money;
        public int Score;

        public CompanyCard(CompanyCardType type, double money)
        {
            Type = type;
            Money = money;
            Score = 0;
        }
    }
}
