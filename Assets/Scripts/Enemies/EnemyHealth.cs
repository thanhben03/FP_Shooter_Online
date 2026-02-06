using Photon.Pun;
using UnityEngine;

public class EnemyHealth : MonoBehaviourPun
{
    [SerializeField] private float maxHp = 100f;
    [SerializeField] private float hp;
    [SerializeField] private GameObject explosionPrefab;

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

    }

    void Die()
    {

        photonView.RPC(nameof(RPC_PlayExplosion), RpcTarget.All, transform.position);
        EnemySpawner.Instance.SetEnemyCountProperyInCurrentRoom(-1);
        PhotonNetwork.Destroy(gameObject);

    }

    [PunRPC]
    void RPC_PlayExplosion(Vector3 pos)
    {
        Instantiate(explosionPrefab, pos, Quaternion.identity);
    }

}
