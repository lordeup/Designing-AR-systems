using Photon.Pun;
using UnityEngine;

namespace Player
{
    public class PlayerControlManager : MonoBehaviour
    {
        private PhotonView _photonView;

        private void Start()
        {
            _photonView = GetComponent<PhotonView>();
        }

        public Vector3 GetPlayerPosition()
        {
            return _photonView.transform.position;
        }

        public void SetPlayerPosition(Vector3 newPosition)
        {
            if (!_photonView.IsMine) return;
            _photonView.transform.position = newPosition;
        }
    }
}
