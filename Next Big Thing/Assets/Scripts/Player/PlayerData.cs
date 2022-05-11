using Card;
using UnityEngine;

namespace Player
{
    public class PlayerData
    {
        public readonly GameObject Player;
        public readonly FounderCard FounderCard;

        public PlayerData(GameObject player, FounderCard founderCard)
        {
            Player = player;
            FounderCard = founderCard;
        }
    }
}
