using Cinemachine;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] LayerMask interactionLayers;

    CinemachineImpulseSource impluseSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(WeaponSO weaponSO)
    {
        RaycastHit hit;

        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, interactionLayers, QueryTriggerInteraction.Ignore);
        muzzleFlash.Play();
        //impluseSource.GenerateImpulse();
        if (hit.collider != null)
        {

            Instantiate(weaponSO.hitVFX, hit.point, Quaternion.identity);
            //EnemyHealth enemyHealth = hit.collider.GetComponentInParent<EnemyHealth>();

            //enemyHealth?.TakeDamage(weaponSO.Damage);
        }
    }
}
