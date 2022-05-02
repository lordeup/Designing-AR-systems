using System;
using UnityEngine;

public class CardRecognizer : MonoBehaviour
{
    public void AddListenerToCard(Action<GameCard> onCardFoundEvent)
    {
        var trackableEventHandlers = transform.GetComponentsInChildren<TrackableEventHandler>();
        foreach (var trackableEventHandler in trackableEventHandlers)
        {
            trackableEventHandler.onTargetFound.AddListener(onCardFoundEvent.Invoke);
        }
    }
}
