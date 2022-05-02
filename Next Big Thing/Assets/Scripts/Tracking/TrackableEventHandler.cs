using UnityEngine;

namespace Tracking
{
    public abstract class TrackableEventHandler<T> : DefaultObserverEventHandler
    {
        [HideInInspector] public CoordinateEvent<T> onTargetFound;

        public T card;

        protected TrackableEventHandler()
        {
            onTargetFound = new CoordinateEvent<T>();
        }

        protected override void OnTrackingFound()
        {
            onTargetFound.Invoke(card);
            base.OnTrackingFound();
        }
    }
}
