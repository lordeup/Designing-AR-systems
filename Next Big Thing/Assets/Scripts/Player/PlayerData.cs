using Card;
using UnityEngine;

namespace Player
{
    public class PlayerData
    {
        public readonly GameObject Player;
        public readonly CompanyCard CompanyCard;
        public readonly FounderCard FounderCard;

        public PlayerData(GameObject player, CompanyCard companyCard, FounderCard founderCard)
        {
            Player = player;
            CompanyCard = companyCard;
            FounderCard = founderCard;
        }
    }
}
