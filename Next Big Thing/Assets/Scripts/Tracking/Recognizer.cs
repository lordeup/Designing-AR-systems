using System;
using UnityEngine;

namespace Tracking
{
    public abstract class Recognizer<T> : MonoBehaviour
    {
        private TrackableEventHandler<T>[] _trackableEventHandlers;

        private void Start()
        {
            _trackableEventHandlers = transform.GetComponentsInChildren<TrackableEventHandler<T>>();
        }

        public void AddListenerToCard(Action<T> onFoundEvent)
        {
            foreach (var trackableEventHandler in _trackableEventHandlers)
            {
                trackableEventHandler.onTargetFound.AddListener(onFoundEvent.Invoke);
            }
        }

        public void RemoveAllListeners()
        {
            foreach (var trackableEventHandler in _trackableEventHandlers)
            {
                trackableEventHandler.onTargetFound.RemoveAllListeners();
            }
        }
    }
}
