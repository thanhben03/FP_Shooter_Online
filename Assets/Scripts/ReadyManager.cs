using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;

public class ReadyManager : MonoBehaviourPunCallbacks
{
    public static ReadyManager Instance;
    public const string READY_KEY = "ready";

    public event Action<int, bool> OnReadyChanged;
    private const string GAME_STARTED_KEY = "GameStarted";

    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ToggleReady()
    {
        if (!PhotonNetwork.InRoom) return;

        bool currentReady = IsReady(PhotonNetwork.LocalPlayer);

        Hashtable ht = new Hashtable();
        ht[READY_KEY] = !currentReady;
        PhotonNetwork.LocalPlayer.SetCustomProperties(ht);
    }

    private bool IsReady(Player p)
    {
        if (p.CustomProperties.TryGetValue(READY_KEY, out object v))
            return (bool)v;

        return false;
    }

    private void CheckAllReadyAndStart()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        foreach (Player p in PhotonNetwork.PlayerList)
        {
            if (!IsReady(p))
                return;
        }
        Hashtable props = new Hashtable();
        props[GAME_STARTED_KEY] = true;
        PhotonNetwork.CurrentRoom.SetCustomProperties(props);

        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
        PhotonNetwork.LoadLevel("MainLevel");
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (changedProps.ContainsKey(READY_KEY))
        {
            OnReadyChanged?.Invoke(targetPlayer.ActorNumber, (bool)changedProps[READY_KEY]);
            if (PhotonNetwork.IsMasterClient)
            {
                CheckAllReadyAndStart();
            }
        }
    }

}
