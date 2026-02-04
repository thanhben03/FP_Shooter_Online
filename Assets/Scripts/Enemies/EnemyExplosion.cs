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
            }
        }
    }
}
