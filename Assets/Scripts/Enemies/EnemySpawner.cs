using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviourPunCallbacks
{
    public static EnemySpawner Instance { get; private set; }
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float spawnTime = 2f;
    public int spawnedCount = 0;

    public int SpawnedCount => spawnedCount;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        if (GameManager.Instance.IsGameOver) return;

        StartCoroutine(SpawnEnemy());
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(SpawnEnemy());
        }
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
            SetEnemyCountProperyInCurrentRoom(1);
            PhotonNetwork.Instantiate(enemyPrefab.name, sp.position, sp.rotation);
            yield return new WaitForSeconds(spawnTime);
        }
    }

    public void SetEnemyCountProperyInCurrentRoom(int value)
    {
        if (!PhotonNetwork.InRoom) return;

        int current = 0;
        if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("EnemyCount", out object count))
            current = (int)count;

        int newValue = current + value;

        var props = new ExitGames.Client.Photon.Hashtable();
        props["EnemyCount"] = newValue;
        spawnedCount = newValue;
        if (spawnedCount <= 0)
        {
            GameManager.Instance.HandleGameWin();
        }
        PhotonNetwork.CurrentRoom.SetCustomProperties(props);
    }

}
