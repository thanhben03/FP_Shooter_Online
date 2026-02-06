using Photon.Pun;
using UnityEngine;

public class EnemyExplosion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnemyHealth enemyHealth = GetComponentInParent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.RequestDamage(9999);
                PhotonView playerPV = other.GetComponent<PhotonView>();
                if (!playerPV.IsMine) return;

                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                playerHealth.TakeDamage(3);
                
            }
        }
    }
}
