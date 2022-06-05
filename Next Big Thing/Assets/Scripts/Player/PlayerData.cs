using Card;
using UnityEngine;

namespace Player
{
    public class PlayerData
    {
        public readonly GameObject Player;
        public readonly CompanyCard CompanyCard;

        public PlayerData(GameObject player, CompanyCard companyCard)
        {
            Player = player;
            CompanyCard = companyCard;
        }
    }
}
