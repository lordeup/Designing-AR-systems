using UnityEngine;

namespace Tracking
{
    public class TrackableEventHandler<T> : DefaultObserverEventHandler
    {
        [HideInInspector] public CoordinateEvent<T> onTargetFound;

        public T card;

        private TrackableEventHandler()
        {
            onTargetFound = new CoordinateEvent<T>();
        }

        protected override void OnTrackingFound()
        {
            Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! " + card);
            onTargetFound.Invoke(card);
            base.OnTrackingFound();
        }
    }
}
