using System;
using UnityEngine.Events;

namespace Tracking
{
    [Serializable]
    public class CoordinateEvent<T> : UnityEvent<T>
    {
    }
}
