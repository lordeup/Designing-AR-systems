using System.Collections.Generic;
using Photon.Pun;
using Room;
using UnityEngine;

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

            if (!SceneController.IsMobile) return;

            _gameController.enabled = false;
            _field.SetActive(false);
        }

        private void Update()
        {
            if (SceneController.IsMobile)
            {
                // HidingPlayers();
            }
        }

        private void HidingPlayers()
        {
            if (PhotonNetwork.IsMasterClient || !gameObject.scene.isLoaded) return;

            _photonViews = FindPhotonViews();
            SetActivePhotonViews(_photonViews, false);
        }

        protected override void OnTrackingLost()
        {
            if (mObserverBehaviour)
            {
                // _photonViews = FindPhotonViews();
                // SetActivePhotonViews(_photonViews, false);
            }

            OnTargetLost?.Invoke();
        }

        protected override void OnTrackingFound()
        {
            if (mObserverBehaviour && SceneController.IsMobile)
            {
                var gameControllerTransform = _gameController.transform;
                var trackableTransform = mObserverBehaviour.transform;

                gameControllerTransform.parent = trackableTransform.parent;
                gameControllerTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                gameControllerTransform.localPosition = Vector3.zero;
                gameControllerTransform.localRotation = Quaternion.identity;

                _gameController.enabled = true;
                _field.SetActive(true);

                // SetActivePhotonViews(_photonViews, true);
            }

            OnTargetFound?.Invoke();
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
