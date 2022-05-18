using ExitGames.Client.Photon;
using Photon.Pun;
using PhotonPlayer = Photon.Realtime.Player;

namespace Room
{
    public static class CustomPropertyUtils
    {
        public static object GetPlayerCustomPropertyByKey(CustomPropertyKeys key, PhotonPlayer player)
        {
            player.CustomProperties.TryGetValue(key.ToString(), out var value);
            return value;
        }

        public static object GetCustomPropertyByKey(CustomPropertyKeys key)
        {
            PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(key.ToString(), out var value);
            return value;
        }

        public static void UpdateCustomPropertyByKey(CustomPropertyKeys key, object value)
        {
            PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable { { key.ToString(), value } });
        }
    }
}
