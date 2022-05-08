using System.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Room
{
    public class SceneController : MonoBehaviourPunCallbacks
    {
        public static void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public delegate void DelegateWaitMethod();

        public static IEnumerator WaitMethod(DelegateWaitMethod method, float second)
        {
            yield return new WaitForSeconds(second);
            method?.Invoke();
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
