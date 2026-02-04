using Photon.Pun;
using StarterAssets;
using System.Collections;
using UnityEngine;
using UnityEngine.ProBuilder;
using static UnityEngine.Rendering.DebugUI.Table;

public class Turret : MonoBehaviourPun
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] FirstPersonController target;
    [SerializeField] Transform turretHead;
    [SerializeField] Transform projectileSpawnPoint;

    [Header("Stats")]
    [SerializeField] float fireRate = 0.3f;
    [SerializeField] float range = 25f;
    [SerializeField] int damage = 2;

    float fireTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        FindNearestPlayer();
        if (target == null) return;

        // aim
        turretHead.LookAt(target.transform.position);

        // bắn theo fireRate
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            fireTimer = fireRate;
            Fire();
        }

    }


    void Fire()
    {
        
        photonView.RPC(nameof(RPC_SpawnProjectile), RpcTarget.All);


    }

    IEnumerator FireRoutine()
    {
        while (target)
        {
            yield return new WaitForSeconds(fireRate);

            photonView.RPC(nameof(RPC_SpawnProjectile), RpcTarget.All);


        }

        yield return null;
    }

    [PunRPC]
    void RPC_SpawnProjectile()
    {
        GameObject projectileObj = Instantiate(projectilePrefab, projectileSpawnPoint.position, turretHead.rotation);
        Projectile projectile = projectileObj.GetComponent<Projectile>();
        projectile.Init(damage);
        //if (playerHealth)
        //{
        //    projectile.transform.LookAt(playerHealth.transform.position);

        //}
    }


    void FindNearestPlayer()
    {
        var players = FindObjectsByType<FirstPersonController>(FindObjectsSortMode.None);

        float minDist = Mathf.Infinity;
        FirstPersonController nearest = null;

        foreach (var p in players)
        {

            float d = Vector3.Distance(transform.position, p.transform.position);
            if (d < minDist)
            {
                minDist = d;
                nearest = p;
            }
        }

        target = nearest;
    }
}
