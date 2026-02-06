using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class SelectSingleColor : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Button chooseBtn;
    public const string COLOR_KEY = "color";


    public void SetData(Color c, int index)
    {
        image.color = c;
        chooseBtn.onClick.AddListener(() =>
        {
            SetColor(index);
        });
    }

    void SetColor(int index)
    {
        Hashtable ht = new Hashtable();
        ht[COLOR_KEY] = index;
        PhotonNetwork.LocalPlayer.SetCustomProperties(ht);
    }
}
