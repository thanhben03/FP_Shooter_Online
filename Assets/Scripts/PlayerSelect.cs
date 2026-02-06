using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelect : MonoBehaviour
{
    [SerializeField] public int actorNumber;
    [SerializeField] TextMeshPro nickNameText;
    [SerializeField] TextMeshPro readyText;

    Renderer rend;
    MaterialPropertyBlock block;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        block = new MaterialPropertyBlock();
    }
    public int Actornumber => actorNumber;

    public void SetData(Player player)
    {
        nickNameText.text = player.NickName;
        actorNumber = player.ActorNumber;
        SetReady(false);

        int colorIndex = 0;
        if (player.CustomProperties.TryGetValue("color", out object colorObj))
        {
            colorIndex = (int)colorObj;
        }
        SetColor(GameManager.Instance.Colors[colorIndex]);
    }

    public void SetReady(bool isReady)
    {
        Color color = !isReady ? Color.red : Color.green;
        string text = !isReady ? "Not ready" : "Ready";

        readyText.text = text;
        readyText.color = color;
    }

    public void SetColor(Color c)
    {
        rend.GetPropertyBlock(block);
        block.SetColor("_BaseColor", c);
        rend.SetPropertyBlock(block);
    }
}
