using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviourPun
{

    public ActiveWeapon activeWeapon;
    [SerializeField] private Animator gunAnimator;

    private void Start()
    {
        
    }

    public void RequestFire()
    {
        if (!photonView.IsMine) return;

        activeWeapon.HandleShoot();
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
    }

    public void TakeDamage(float damage)
    {

    }
}
