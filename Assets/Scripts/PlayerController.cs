using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviourPun
{
    public ActiveWeapon activeWeapon;
    [SerializeField] private Animator gunAnimator;

    private void Start()
    {
        if (!photonView.IsMine) return;

        photonView.RPC(nameof(RPC_EquipWeapon), RpcTarget.AllBuffered, 0);

        if (!gunAnimator)
            gunAnimator = GetComponentInChildren<Animator>();
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

    [PunRPC]
    void RPC_EquipWeapon(int weaponId)
    {
        activeWeapon.EquipWeaponLocal(weaponId);
    }
}
