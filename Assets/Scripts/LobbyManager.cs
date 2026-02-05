using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public static LobbyManager Instance;
    public TextMeshProUGUI statusText;

    public TMP_InputField inputRoomName;

    public event Action<Dictionary<string, RoomInfo>> OnAnyPlayerCreatedRoom;
    public event Action OnAnyPlayerJoinedRoom;
    public event Action OnAnyPlayerLeftRoom;

    private Dictionary<string, RoomInfo> cachedRoomList = new();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();

    }

    public void CreateRoom()
    {
        if (!PhotonNetwork.IsConnectedAndReady)
        {
            statusText.text = "Not connected yet!";
            return;
        }
        string roomName = inputRoomName.text.Trim();

        if (string.IsNullOrEmpty(roomName))
        {
            statusText.text = "Room name is empty!";
            return;
        }

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        options.EmptyRoomTtl = 0;

        statusText.text = "Creating room: " + roomName;
        PhotonNetwork.CreateRoom(roomName, options);
    }

    public void JoinRoom(string roomName)
    {
        if (string.IsNullOrEmpty(roomName))
        {
            statusText.text = "Room name is empty!";
            return;
        }

        statusText.text = "Joining room: " + roomName;

        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnConnectedToMaster()
    {

        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("SelectCharacter");

    }

    public void JoinRandom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom("Asernal");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (var room in roomList)
        {
            if (room.RemovedFromList || room.PlayerCount == 0)
            {
                cachedRoomList.Remove(room.Name);
            }
            else
            {
                cachedRoomList[room.Name] = room;
            }
        }
        OnAnyPlayerCreatedRoom.Invoke(cachedRoomList);
    }



}
