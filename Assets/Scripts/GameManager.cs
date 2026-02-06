using Cinemachine;
using Photon.Pun;
using StarterAssets;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviourPun
{
    public static GameManager Instance;
    public bool isGameOver;
    public bool IsGameOver => isGameOver;
    public Color[] Colors => colors;
    public Dictionary<int, PlayerData> playerList;
    [SerializeField] Color[] colors;
    [SerializeField] CinemachineVirtualCamera deathCam;

    [System.Serializable]
    public class PlayerData
    {
        public bool isLive;
    }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        playerList = new Dictionary<int, PlayerData>();
    }

    //[PunRPC]
    //public void GameLose()
    //{
    //    if(!PhotonNetwork.IsMasterClient) return;

    //    bool isDiedAll = true;

    //    // neu co nguoi choi con song
    //    foreach (var pair in playerList)
    //    {
    //        int actorNumber = pair.Key;
    //        PlayerData playerData = pair.Value;

    //        if (playerData.isLive)
    //        {
    //            isDiedAll = false;
    //            break;
    //        }
    //    }

    //    if (!isDiedAll)
    //    {

    //    }

    //    // tat ca da die



    //    deathCam = GameObject.FindGameObjectWithTag("DeathCam").GetComponent<CinemachineVirtualCamera>();
    //    isGameOver = true;
    //    deathCam.Priority = 20;
    //    SetCursorState(false);
    //    ResultUI.Instance.GameLose();
    //}

    //[PunRPC]
    //void RPC_ReportDeath(int actorNumber)
    //{
    //    if (!PhotonNetwork.IsMasterClient) return;

    //    playerList[actorNumber].isLive = false;

    //    photonView.RPC(nameof(RPC_PlayerLose), PhotonNetwork.CurrentRoom.GetPlayer(actorNumber));

    //    CheckGameLose();
    //}

    public bool IsDiedAll()
    {
        bool isDiedAll = true;
        playerList[PhotonNetwork.LocalPlayer.ActorNumber].isLive = false;
        foreach (var pair in playerList)
        {
            PlayerData playerData = pair.Value;

            if (playerData.isLive)
            {
                isDiedAll = false;
                break;
            }
        }

        return isDiedAll;
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

    public void SetCursorState(bool status)
    {
        StarterAssetsInputs starterAssetsInputs = FindAnyObjectByType<StarterAssetsInputs>();
        starterAssetsInputs.SetCursorState(status);
    }

    public void SetPlayerData(int actorNumber)
    {
        if (!PhotonNetwork.IsMasterClient) return;
        playerList.Add(actorNumber, new PlayerData { isLive = true });
    }
}
