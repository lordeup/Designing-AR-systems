using Photon.Pun;
using UnityEngine.SceneManagement;

namespace Room
{
    public class SceneController : MonoBehaviourPunCallbacks
    {
        public static void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public override void OnLeftRoom()
        {
            LoadScene("Lobby");
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }
    }
}
