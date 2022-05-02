using System;
using UnityEngine;

namespace Tracking
{
    public class Recognizer<T> : MonoBehaviour
    {
        public void AddListenerToCard(Action<T> onFoundEvent)
        {
            var trackableEventHandlers = transform.GetComponentsInChildren<TrackableEventHandler<T>>();
            foreach (var trackableEventHandler in trackableEventHandlers)
            {
                trackableEventHandler.onTargetFound.AddListener(onFoundEvent.Invoke);
            }
        }
    }
}
