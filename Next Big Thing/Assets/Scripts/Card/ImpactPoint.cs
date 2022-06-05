using Card.Type;

namespace Card
{
    public class ImpactPoint
    {
        public ImpactPointType Type;
        public string Description;
        public int Score;

        public ImpactPoint(ImpactPointType type, string description, int score)
        {
            Type = type;
            Description = description;
            Score = score;
        }
    }
}
