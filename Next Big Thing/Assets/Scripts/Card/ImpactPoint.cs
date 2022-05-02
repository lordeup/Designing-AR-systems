using Card.Type;

namespace Card
{
    public class ImpactPoint
    {
        public ImpactPoint(ImpactPointType type, string description, double point)
        {
            Type = type;
            Description = description;
            Point = point;
        }

        public ImpactPointType Type { get; set; }

        public string Description { get; set; }

        public double Point { get; set; }
    }
}
