using Card.Type;

namespace Card
{
    public class CompanyCard
    {
        public CompanyCardType Type;
        public double Money;
        public double Score;

        public CompanyCard(CompanyCardType type, double money)
        {
            Type = type;
            Money = money;
            Score = 100;
        }
    }
}
