using Card.Type;

namespace Card
{
    public class ImpactPoint
    {
        public ImpactPointType PointType;
        public string Description;

        public ImpactPoint(ImpactPointType pointType, string description)
        {
            PointType = pointType;
            Description = description;
        }
    }
}
