using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class SelectCharacterMenuUI : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI roomNameText;
    public GameObject[] characters;
    private const string SLOT_KEY = "slot";

    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateUI()
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            characters[i].gameObject.SetActive(true);
            Player player = PhotonNetwork.PlayerList[i];

            characters[i].gameObject.GetComponent<PlayerSelect>().SetData(player);

        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player joined: " + newPlayer.NickName + " | ActorNumber: " + newPlayer.ActorNumber);
        UpdateUI();

    }

}
