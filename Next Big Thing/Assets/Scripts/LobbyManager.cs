using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private const string GameSceneName = "Game";

    // TODO remove
    // private MazeGenerator _mazeGenerator;

    // private void Start()
    // {
    //     _mazeGenerator = gameObject.AddComponent<MazeGenerator>();
    // }

    public void CreateRoom()
    {
        // _mazeGenerator.Initialize();
        var roomOptions = new RoomOptions
        {
            MaxPlayers = 2,
            // PublishUserId = true
            // CustomRoomProperties = _mazeGenerator.GenerateCustomRoomProperties()
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
