using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Room
{
    public class LobbyManager : MonoBehaviourPunCallbacks
    {
        private const string GameSceneName = "Game";

        // TODO
        // private GameStorage _gameStorage;
        //
        // private void Start()
        // {
        //     _gameStorage = gameObject.AddComponent<GameStorage>();
        // }

        public void CreateRoom()
        {
            // _gameStorage.Initialize();
            var roomOptions = new RoomOptions
            {
                MaxPlayers = 2,
                // CustomRoomProperties = _gameStorage.GenerateCustomRoomProperties()
                // PublishUserId = true
            };

            PhotonNetwork.CreateRoom(null, roomOptions);
            Debug.Log("Room created");
        }

        public void JoinRoom()
        {
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("joined room");

            PhotonNetwork.LoadLevel(GameSceneName);
        }
    }
}
