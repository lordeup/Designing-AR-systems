using System;
using Photon.Pun;
using UnityEngine;

namespace Player
{
    public class PlayerUtils : MonoBehaviour
    {
        [SerializeField] private Transform prefabPlayer1;
        [SerializeField] private Transform prefabPlayer2;

        public static readonly Vector3 InitPlayerPosition = new(0, 0.03f, 0);

        public GameObject InitializationPlayer()
        {
            var playerType = GetPlayerType();
            var player = GetPlayer(playerType);
            return PhotonNetwork.Instantiate(player.name, InitPlayerPosition, Quaternion.identity);
        }

        private static PlayerType GetPlayerType()
        {
            return PhotonNetwork.IsMasterClient ? PlayerType.Player1 : PlayerType.Player2;
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
