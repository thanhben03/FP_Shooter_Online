using Cinemachine;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] CinemachineVirtualCamera playerFollowCamera;
    public static GameManager Instance;
    public CinemachineVirtualCamera PlayerFollowCamera => PlayerFollowCamera;
    public TextMeshProUGUI statusText;

    public TMP_InputField inputRoomName;

    public event Action<List<RoomInfo>> OnAnyPlayerCreatedRoom;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
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

    public void SetPlayerFollowCamera(CinemachineVirtualCamera playerFollowCamera)
    {
        this.playerFollowCamera = playerFollowCamera;
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
        OnAnyPlayerCreatedRoom.Invoke(roomList);   
    }

}
