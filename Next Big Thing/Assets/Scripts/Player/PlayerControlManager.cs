using Photon.Pun;
using UnityEngine;

namespace Player
{
    public class PlayerControlManager : MonoBehaviour
    {
        protected Vector3 InitPosition;

        private Camera _mainCamera;
        private PhotonView _photonView;

        private void Start()
        {
            _mainCamera = Camera.main;
            _photonView = GetComponent<PhotonView>();
        }

        private void Update()
        {
            if (!_photonView.IsMine)
            {
                return;
            }

            UpdatePlayer();
        }

        private void UpdatePlayer()
        {
        }
    }
}
