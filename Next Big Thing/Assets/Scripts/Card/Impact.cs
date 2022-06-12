using System.Collections.Generic;
using Card.Type;

namespace Card
{
    public class Impact
    {
        public ImpactPointType PointType;
        public List<ImpactValue> ImpactValues;
        public string Description;

        public Impact(ImpactPointType pointType, List<ImpactValue> impactValues, string description)
        {
            PointType = pointType;
            ImpactValues = impactValues;
            Description = description;
        }
    }
}
