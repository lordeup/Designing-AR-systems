using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Room;
using UnityEngine;
using PhotonPlayer = Photon.Realtime.Player;

namespace Player
{
    public class PlayerUtils : MonoBehaviour
    {
        [SerializeField] private Transform prefabPlayer1;
        [SerializeField] private Transform prefabPlayer2;

        public static readonly Vector3 InitPlayerPosition = new(0f, 0.03f, 0f);

        public GameObject InitializationPlayer()
        {
            var playerType = GetPlayerType(GetLocalPlayer());
            var player = GetPlayer(playerType);
            return PhotonNetwork.Instantiate(player.name, InitPlayerPosition, Quaternion.identity);
        }

        public PhotonPlayer GetMasterPlayer()
        {
            var players = GetPlayers();
            return players.First(player => player.IsMasterClient);
        }

        public PhotonPlayer GetPlayerById(string id)
        {
            var players = GetPlayers();
            return players.First(player => player.UserId == id);
        }

        public PhotonPlayer GetPlayerByType(PlayerType type)
        {
            var players = GetPlayers();
            return players.First(player => GetPlayerType(player) == type);
        }

        public Dictionary<string, double> GetPlayerMoneyCustomProperties()
        {
            return (Dictionary<string, double>)CustomPropertyUtils.GetCustomPropertyByKey(
                CustomPropertyKeys.PlayerMoney);
        }

        public Dictionary<string, int> GetPlayerScoreCustomProperties()
        {
            return (Dictionary<string, int>)CustomPropertyUtils.GetCustomPropertyByKey(
                CustomPropertyKeys.PlayerScore);
        }

        public PhotonPlayer GetLocalPlayer()
        {
            return PhotonNetwork.LocalPlayer;
        }

        public PlayerType GetPlayerType(PhotonPlayer player)
        {
            return (PlayerType)CustomPropertyUtils.GetPlayerCustomPropertyByKey(CustomPropertyKeys.PlayerType, player);
        }

        public IEnumerable<PhotonPlayer> GetPlayers()
        {
            return PhotonNetwork.PlayerList;
        }

        private Transform GetPlayer(PlayerType playerType)
        {
            return playerType switch
            {
                PlayerType.Player1 => prefabPlayer1,
                PlayerType.Player2 => prefabPlayer2,
                _ => throw new ArgumentOutOfRangeException(nameof(playerType), playerType, null)
            };
        }
    }
}
