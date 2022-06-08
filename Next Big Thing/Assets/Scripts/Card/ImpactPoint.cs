using System.Collections.Generic;
using Card.Type;

namespace Card
{
    public class ImpactPoint
    {
        public ImpactPointType PointType;
        public string Description;
        public List<Impact> Impacts;

        public ImpactPoint(ImpactPointType pointType, List<Impact> impacts, string description)
        {
            PointType = pointType;
            Impacts = impacts;
            Description = description;
        }
    }
}
