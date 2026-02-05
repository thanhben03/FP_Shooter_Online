using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelect : MonoBehaviour
{
    [SerializeField] TextMeshPro nickNameText;
    public void SetData(Player player)
    {
        nickNameText.text = player.NickName;
    }
}
