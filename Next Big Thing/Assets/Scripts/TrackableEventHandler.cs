using UnityEngine;

public class TrackableEventHandler : DefaultObserverEventHandler
{
    [HideInInspector] public CoordinateEvent onTargetFound;

    public GameCard card;

    private TrackableEventHandler()
    {
        onTargetFound = new CoordinateEvent();
    }

    protected override void OnTrackingFound()
    {
        Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! " + card);
        onTargetFound.Invoke(card);
        base.OnTrackingFound();
    }
}
