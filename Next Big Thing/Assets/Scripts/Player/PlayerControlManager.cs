using Photon.Pun;
using UnityEngine;

namespace Player
{
    public class PlayerControlManager : MonoBehaviour
    {
        private PhotonView _photonView;

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
        }

        public void SetPlayerPosition(Vector3 newPosition)
        {
            if (!_photonView.IsMine) return;
            _photonView.transform.position = newPosition;
        }
    }
}
