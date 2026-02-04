using Photon.Pun;
using UnityEngine;

public class EnemyHealth : MonoBehaviourPun
{
    [SerializeField] private float maxHp = 100f;
    [SerializeField] private float hp;

    public float HP => hp;
    public float MaxHP => maxHp;
    void Start()
    {
        hp = maxHp;
    }

    public void RequestDamage(float damage)
    {
        if (damage <= 0) return;

        photonView.RPC(nameof(RPC_ApplyDamage), RpcTarget.MasterClient, damage);
    }


    [PunRPC]
    void RPC_ApplyDamage(float damage, PhotonMessageInfo info)
    {
        if (!PhotonNetwork.IsMasterClient) return;

        if (hp <= 0) return;

        hp -= damage;
        hp = Mathf.Clamp(hp, 0, maxHp);

        photonView.RPC(nameof(RPC_SyncHP), RpcTarget.All, hp);

        if (hp <= 0)
        {
            Die();
        }
    }

    [PunRPC]
    void RPC_SyncHP(float newHp)
    {
        hp = newHp;

        // TODO: update UI / healthbar
    }

    void Die()
    {

        PhotonNetwork.Destroy(gameObject);
    }

}
