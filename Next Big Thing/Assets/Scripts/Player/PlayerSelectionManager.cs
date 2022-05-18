using Room;
using UnityEngine;

namespace Player
{
    public class PlayerSelectionManager : MonoBehaviour
    {
        private const string LobbySceneName = "Lobby";

        private static PlayerType _playerType = PlayerType.Player1;

        public static PlayerType GetPlayerType()
        {
            return _playerType;
        }

        public void SetPlayer1()
        {
            SetPlayerType(PlayerType.Player1);
        }

        public void SetPlayer2()
        {
            SetPlayerType(PlayerType.Player2);
        }

        private static void SetPlayerType(PlayerType type)
        {
            _playerType = type;
            SceneController.LoadScene(LobbySceneName);
        }
    }
}
