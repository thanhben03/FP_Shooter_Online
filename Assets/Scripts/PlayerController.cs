using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviourPun
{

    public ActiveWeapon activeWeapon;
    [SerializeField] private Animator gunAnimator;
    [SerializeField] GameObject playerCapsule;
    Renderer rend;
    MaterialPropertyBlock block;

    private void Awake()
    {
        if (playerCapsule == null)
        {
            Debug.LogError("playerCapsule chưa được gán trong Inspector!");
            return;
        }

        rend = playerCapsule.GetComponent<Renderer>();
        if (rend == null)
        {
            Debug.LogError("playerCapsule không có Renderer!");
            return;
        }

        block = new MaterialPropertyBlock();
    }
    private void Start()
    {
        ApplyColor();
    }

    public void RequestFire()
    {
        if (!photonView.IsMine) return;

        photonView.RPC(nameof(RPC_FireEffects), RpcTarget.All);

    }

    [PunRPC]
    private void RPC_FireEffects()
    {
        activeWeapon.PlayFireEffectsOnly();
    }

    public void AddAmmo()
    {
        AmmoUI.Instance.UpdateUI(30);
        activeWeapon.AdjustAmmo(30);
    }

    public void TakeDamage(float damage)
    {

    }

    public void ApplyColor()
    {
        int indexColor = 0;
        if (photonView.Owner.CustomProperties.TryGetValue("color", out object value))
        {
            indexColor = (int)value;
        }

        Color c = GameManager.Instance.Colors[indexColor];

        rend.GetPropertyBlock(block);
        block.SetColor("_BaseColor", c);
        rend.SetPropertyBlock(block);
    }
}
