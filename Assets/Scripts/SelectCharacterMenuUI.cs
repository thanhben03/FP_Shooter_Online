using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectCharacterMenuUI : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI roomNameText;
    public GameObject[] characters;
   
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        if (PhotonNetwork.InRoom)
        {
            roomNameText.text = "Room: " + PhotonNetwork.CurrentRoom.Name;

            UpdateUI();
        }
        else
        {
            roomNameText.text = "Not in room!";
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            characters[i].gameObject.SetActive(false);

        }
        Debug.Log(PhotonNetwork.PlayerList.Length);
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            characters[i].SetActive(true);
            Player player = PhotonNetwork.PlayerList[i];
            Debug.Log(player.NickName);
            characters[i].GetComponent<PlayerSelect>().SetData(player);

        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player joined: " + newPlayer.NickName + " | ActorNumber: " + newPlayer.ActorNumber);
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
