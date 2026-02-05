using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI roomNameText;
    [SerializeField] Button joinButton;

    private void Start()
    {
        joinButton.onClick.AddListener(() =>
        {
            LobbyManager.Instance.JoinRoom(roomNameText.text);
        });
    }

    public void Init(string roomName) {  roomNameText.text = roomName; }
}
