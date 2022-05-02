using Card.Type;

namespace Card
{
    public class CompanyCard
    {
        public CompanyCard(CompanyCardType type, double money)
        {
            Type = type;
            Money = money;
        }

        public CompanyCardType Type { get; set; }

        public double Money { get; set; }
    }
}
