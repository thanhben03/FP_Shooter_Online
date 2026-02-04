using Photon.Pun;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float spawnTime = 2f;

    void Start()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];

            PhotonNetwork.Instantiate(enemyPrefab.name, sp.position, sp.rotation);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
