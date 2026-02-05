using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] Transform roomContainer;
    [SerializeField] GameObject roomPrefab;
    [SerializeField] TextMeshProUGUI helloText;
    [SerializeField] TMP_InputField inputNickName;
    [SerializeField] Transform setNamePopup;

    void Start()
    {
        LobbyManager.Instance.OnAnyPlayerCreatedRoom += LobbyManager_OnAnyPlayerCreatedRoom;
    }

    private void LobbyManager_OnAnyPlayerCreatedRoom(Dictionary<string, RoomInfo> list)
    {
        foreach (Transform child in roomContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (var pair in list)
        {
            string roomName = pair.Key;
            RoomInfo roomInfo = pair.Value;

            GameObject roomObj = Instantiate(roomPrefab, roomContainer);
            roomObj.GetComponent<RoomItem>().Init(roomInfo.Name);

        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNickName()
    {
        helloText.text = "Hello, " + inputNickName.text;
        PlayerPrefs.SetString("nickname", inputNickName.text);
        PhotonNetwork.NickName = PlayerPrefs.GetString("nickname");

        setNamePopup.gameObject.SetActive(false);
    }
}
