using System.Collections.Generic;
using Photon.Pun;
using PhotonPlayer = Photon.Realtime.Player;

namespace Utils
{
    public static class PhotonNetworkUtils
    {
        public static PhotonPlayer GetLocalPlayer()
        {
            return PhotonNetwork.LocalPlayer;
        }
        
        public static string GetLocalUserId()
        {
            return GetLocalPlayer().UserId;
        }

        public static bool IsMine(string userId)
        {
            return GetLocalUserId().Equals(userId);
        }
        
        public static IEnumerable<PhotonPlayer> GetPlayers()
        {
            return PhotonNetwork.PlayerList;
        }
    }
}
