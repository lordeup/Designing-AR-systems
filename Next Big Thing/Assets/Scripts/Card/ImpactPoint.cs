using Card.Type;

namespace Card
{
    public class ImpactPoint
    {
        public ImpactPointType Type;
        public string Description;
        public double Point;

        public ImpactPoint(ImpactPointType type, string description, double point)
        {
            Type = type;
            Description = description;
            Point = point;
        }
    }
}
