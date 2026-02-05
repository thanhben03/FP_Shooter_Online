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
        GameManager.Instance.OnAnyPlayerCreatedRoom += GameManager_OnAnyPlayerCreatedRoom;
    }

    private void GameManager_OnAnyPlayerCreatedRoom(List<RoomInfo> list)
    {
        foreach (RoomInfo roomInfo in list)
        {
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
