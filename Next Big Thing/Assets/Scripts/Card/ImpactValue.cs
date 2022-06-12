using Card.Type;

namespace Card
{
    public class ImpactValue
    {
        public ImpactPointType PointType;
        public ImpactValueType ValueType;
        public ImpactType ImpactType;
        public double Value;

        public ImpactValue(ImpactPointType pointType, ImpactValueType valueType, ImpactType impactType, double value)
        {
            PointType = pointType;
            ValueType = valueType;
            ImpactType = impactType;
            Value = value;
        }
    }
}
