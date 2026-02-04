using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class AmmoSpawner : MonoBehaviourPun
{
    public static AmmoSpawner Instance;
    public Transform[] spawnPoints;
    [SerializeField] private List<Transform> availablePoints = new List<Transform>();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {

        if (PhotonNetwork.IsMasterClient)
        {
            availablePoints.AddRange(spawnPoints);
            for (int i = 0; i < 2; i++)
            {
                StartCoroutine(SpawnAmmo(i));
            }
        }
    }

    IEnumerator SpawnAmmo(int poolId)
    {
        if (availablePoints.Count == 0) yield return null;

        yield return new WaitForSeconds(2);
        int randomIndex = Random.Range(0, availablePoints.Count);
        Transform point = availablePoints[randomIndex];

        int spawnIndex = System.Array.IndexOf(spawnPoints, point);

        availablePoints.RemoveAt(randomIndex);

        photonView.RPC(nameof(RPC_EnableAmmo), RpcTarget.AllBuffered,
            poolId,
            spawnIndex);
    }

    [PunRPC]
    void RPC_EnableAmmo(int poolId, int spawnIndex)
    {
        Transform point = spawnPoints[spawnIndex];

        var ammo = AmmoPoolManager.Instance.GetAmmoByIndex(poolId);
        ammo.transform.position = point.position;
        ammo.SetActive(true);

        AmmoPickup pickup = ammo.GetComponent<AmmoPickup>();
        pickup.poolId = poolId;
        pickup.spawnIndex = spawnIndex;

    }

    [PunRPC]
    void RPC_DisableAmmo(int poolId)
    {
        var ammo = AmmoPoolManager.Instance.GetAmmoByIndex(poolId);
        ammo.SetActive(false);

        var pickup = ammo.GetComponent<AmmoPickup>();

        availablePoints.Add(spawnPoints[pickup.spawnIndex]);

    }

    public void RequestPickup(int poolId, int actorNumber)
    {
        if (!PhotonNetwork.IsMasterClient) return;

        photonView.RPC(nameof(RPC_DisableAmmo), RpcTarget.All, poolId);
        StartCoroutine(SpawnAmmo(poolId));
    }
}
