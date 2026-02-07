using Cinemachine;
using Photon.Pun;
using StarterAssets;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;


public class GameManager : MonoBehaviourPun
{
    public static GameManager Instance;
    public bool isGameOver;
    public bool IsGameOver => isGameOver;
    public Color[] Colors => colors;
    [SerializeField] public Dictionary<int, PlayerData> playerList;
    [SerializeField] Color[] colors;
    [SerializeField] CinemachineVirtualCamera deathCam;
    public const string ROOM_ALIVE_COUNT = "aliveCount";

    [System.Serializable]
    public class PlayerData
    {
        public bool isLive;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        playerList = new Dictionary<int, PlayerData>();

    }

    private void Start()
    {
    }

    private void Update()
    {
        if (IsGameOver)
        {
            SetCursorState(false);
        }
    }

    [PunRPC]
    public void RPC_GameLose()
    {
        isGameOver = true;

        var deathCamObj = GameObject.FindGameObjectWithTag("DeathCam");
        if (deathCamObj != null)
        {
            deathCam = deathCamObj.GetComponent<CinemachineVirtualCamera>();
            if (deathCam != null) deathCam.Priority = 20;
        }

        SetCursorState(false);
        ResultUI.Instance.GameLose();

    }

    public void HandleGameWin()
    {

        photonView.RPC(nameof(RPC_GameWin), RpcTarget.All);
    }

    [PunRPC]
    void RPC_GameWin()
    {
        isGameOver = true;

        var deathCamObj = GameObject.FindGameObjectWithTag("DeathCam");
        if (deathCamObj != null)
        {
            deathCam = deathCamObj.GetComponent<CinemachineVirtualCamera>();
            if (deathCam != null) deathCam.Priority = 20;
        }

        SetCursorState(false);
        ResultUI.Instance.GameWin();
    }

    public void SetCursorState(bool status)
    {
        Cursor.lockState = status ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !status;
    }


    public int CountPlayerLive()
    {
        int alive = -1;

        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(ROOM_ALIVE_COUNT))
            alive = (int)PhotonNetwork.CurrentRoom.CustomProperties[ROOM_ALIVE_COUNT];
        Debug.Log(alive);
        return alive;
    }

    public void DecreaseAliveCount()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        int alive = 0;

        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(ROOM_ALIVE_COUNT))
            alive = (int)PhotonNetwork.CurrentRoom.CustomProperties[ROOM_ALIVE_COUNT];

        alive--;

        var props = new ExitGames.Client.Photon.Hashtable();
        props[ROOM_ALIVE_COUNT] = alive;
        Debug.Log("After one player die: " + alive);
        PhotonNetwork.CurrentRoom.SetCustomProperties(props);
    }
}
