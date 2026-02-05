using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] Animator animator;
    [SerializeField] LayerMask interactionLayers;

    CinemachineImpulseSource impluseSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        impluseSource = GetComponent<CinemachineImpulseSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayEffect()
    {
        muzzleFlash.Play();
        animator.Play("Shot", 0, 0f);
    }

    public void Shoot(WeaponSO weaponSO)
    {
        //PlayEffect();
        RaycastHit hit;

        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, interactionLayers, QueryTriggerInteraction.Ignore);

        impluseSource.GenerateImpulse();
        //impluseSource.GenerateImpulse();
        if (hit.collider != null)
        {

            Debug.Log(hit.collider.name);
            //Instantiate(weaponSO.hitVFX, hit.point, Quaternion.identity);
            PhotonNetwork.Instantiate(weaponSO.hitVFX.name, hit.point, Quaternion.identity);

            EnemyHealth enemyHealth = hit.collider.GetComponentInParent<EnemyHealth>();

            enemyHealth?.RequestDamage(weaponSO.Damage);
        }
    }
}
