using Photon.Pun;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(null,
            new Photon.Realtime.RoomOptions
            {
                MaxPlayers = 2
            });
        Debug.Log("Room created");
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("joined room");

        PhotonNetwork.LoadLevel("Game");
    }
}
