using Photon.Pun;
using UnityEngine;

public class SpawnCharacter : MonoBehaviour
{
    public GameObject character;
    public Transform[] spawnPoint;
    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Instantiate(character.name, spawnPoint[PhotonNetwork.CountOfPlayers - 1].position, spawnPoint[PhotonNetwork.CountOfPlayers - 1].rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
