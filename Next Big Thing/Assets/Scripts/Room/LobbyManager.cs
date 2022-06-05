using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Player;
using UnityEngine;

namespace Room
{
    public class LobbyManager : MonoBehaviourPunCallbacks
    {
        private const string GameSceneName = "Game";
        private const string RoomName = "Room 1";
        private PlayerType _playerType;

        private void Start()
        {
            _playerType = PlayerSelectionManager.GetPlayerType();
        }

        public void JoinOrCreateRoom()
        {
            PhotonNetwork.SetPlayerCustomProperties(GetPlayerCustomProperties());
            var roomOptions = new RoomOptions
            {
                CustomRoomProperties = GetCustomProperties(),
                MaxPlayers = 2,
                PublishUserId = true
            };

            PhotonNetwork.JoinOrCreateRoom(RoomName, roomOptions, TypedLobby.Default);
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("joined room");

            PhotonNetwork.LoadLevel(GameSceneName);
        }

        private Hashtable GetPlayerCustomProperties()
        {
            var properties = new Hashtable
            {
                { CustomPropertyKeys.PlayerType.ToString(), _playerType },
            };
            return properties;
        }

        private static Hashtable GetCustomProperties()
        {
            var dictionaryMoney = new Dictionary<string, double>();
            var dictionaryScore = new Dictionary<string, int>();

            var properties = new Hashtable
            {
                { CustomPropertyKeys.PlayerMoney.ToString(), dictionaryMoney },
                { CustomPropertyKeys.PlayerScore.ToString(), dictionaryScore },
            };
            return properties;
        }
    }
}
