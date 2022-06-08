using Card.Type;

namespace Card
{
    public class Impact
    {
        public ImpactPointType PointType;
        public ImpactType ImpactType;
        public double Value;
        public string Description;

        public Impact(ImpactPointType pointType, ImpactType impactType, double value, string description)
        {
            PointType = pointType;
            ImpactType = impactType;
            Value = value;
            Description = description;
        }
    }
}
