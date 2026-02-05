using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] CinemachineVirtualCamera playerFollowCamera;
    public static GameManager Instance;
    public CinemachineVirtualCamera PlayerFollowCamera => PlayerFollowCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerFollowCamera(CinemachineVirtualCamera playerFollowCamera)
    {
        this.playerFollowCamera = playerFollowCamera;
    }

    public override void OnConnectedToMaster()
    {

        Debug.Log("Im connected to the server");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {

       PhotonNetwork.LoadLevel("MainLevel");
        
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom("Asernal");
    }
}
