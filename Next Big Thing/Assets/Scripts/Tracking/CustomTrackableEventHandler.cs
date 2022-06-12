using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Utils;

namespace Tracking
{
    public class CustomTrackableEventHandler : DefaultObserverEventHandler
    {
        private MultiplayerGameManager _gameController;
        private GameObject _field;
        private PhotonView[] _photonViews;

        private void Awake()
        {
            _gameController = FindObjectOfType<MultiplayerGameManager>();
            _field = GameObject.FindGameObjectWithTag(GameObjectTag.Field.ToString());

            if (!SharedUtils.IsMobile) return;

            _gameController.enabled = false;
            _field.SetActive(false);
        }

        private void Update()
        {
            if (PhotonNetwork.IsMasterClient || !gameObject.scene.isLoaded || !SharedUtils.IsMobile)
            {
                return;
            }

            HidingPlayers();
        }

        protected override void OnTrackingLost()
        {
            if (mObserverBehaviour)
            {
                _gameController.isTrackingFound = false;
                HidingPlayers();
            }

            OnTargetLost?.Invoke();
        }

        protected override void OnTrackingFound()
        {
            if (mObserverBehaviour && SharedUtils.IsMobile)
            {
                var gameControllerTransform = _gameController.transform;
                var trackableTransform = mObserverBehaviour.transform;

                gameControllerTransform.parent = trackableTransform.parent;
                gameControllerTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                gameControllerTransform.localPosition = Vector3.zero;
                gameControllerTransform.localRotation = Quaternion.identity;

                _gameController.isTrackingFound = true;
                _gameController.enabled = true;
                _field.SetActive(true);

                // SetActivePhotonViews(_photonViews, true);
            }

            OnTargetFound?.Invoke();
        }

        private void HidingPlayers()
        {
            // _photonViews = FindPhotonViews();
            // if (_photonViews.Length == 0) return;

            // SetActivePhotonViews(_photonViews, false);
        }

        private static PhotonView[] FindPhotonViews()
        {
            return FindObjectsOfType<PhotonView>();
        }

        private static void SetActivePhotonViews(IEnumerable<PhotonView> views, bool state)
        {
            foreach (var view in views)
            {
                view.gameObject.SetActive(state);
            }
        }
    }
}
