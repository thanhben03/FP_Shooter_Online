using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectCharacterMenuUI : MonoBehaviourPunCallbacks
{
    public static SelectCharacterMenuUI Instance;
    public TextMeshProUGUI roomNameText;
    public PlayerSelect[] characters;
    public const string COLOR_KEY = "color";


    private void Awake()
    {
        Instance = this;
    }
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        if (PhotonNetwork.InRoom)
        {
            roomNameText.text = "Room: " + PhotonNetwork.CurrentRoom.Name;

            UpdateUI();
            ReadyManager.Instance.OnReadyChanged += ReadyManager_OnReadyChanged;
        }
        else
        {
            roomNameText.text = "Not in room!";
        }
    }

    private void ReadyManager_OnReadyChanged(int actorNumber, bool isReady)
    {
        foreach (var p in characters)
        {
            if (p.GetComponent<PlayerSelect>().Actornumber == actorNumber)
            {
                p.GetComponent<PlayerSelect>().SetReady(isReady);
                break;
            }
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].gameObject.SetActive(false);

        }
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            characters[i].gameObject.SetActive(true);
            Player player = PhotonNetwork.PlayerList[i];
            characters[i].SetData(player);

        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (changedProps.ContainsKey(COLOR_KEY))
        {
            int indexColor = (int)changedProps[COLOR_KEY];
            foreach (var p in characters)
            {
                PlayerSelect player = p.GetComponent<PlayerSelect>();
                if (player.Actornumber == targetPlayer.ActorNumber)
                {
                    player.SetColor(GameManager.Instance.Colors[indexColor]);
                    break;
                }
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        LobbyManager.Instance.InitAliveCount();
        UpdateUI();

    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Player left: " + otherPlayer.NickName + " | ActorNumber: " + otherPlayer.ActorNumber);
        UpdateUI();
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log("New Master: " + newMasterClient.NickName);

        if (!PhotonNetwork.InRoom) return;

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;

            PhotonNetwork.LeaveRoom();
        }

    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }
}
