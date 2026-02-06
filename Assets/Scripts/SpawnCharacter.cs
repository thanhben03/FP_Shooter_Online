using Photon.Pun;
using System.Collections;
using UnityEngine;

public class SpawnCharacter : MonoBehaviour
{
    public GameObject character;
    public Transform[] spawnPoint;
    void Start()
    {

        if (PhotonNetwork.IsConnected)
        {
            Debug.Log($"AppId: {PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime}");
            Debug.Log($"Region: {PhotonNetwork.CloudRegion}");
            Debug.Log($"Version: {PhotonNetwork.GameVersion}");
            Debug.Log($"Room: {PhotonNetwork.CurrentRoom?.Name}");

            StartCoroutine(SpawnPlayer());
            //PhotonNetwork.Instantiate(character.name, spawnPoint[PhotonNetwork.CountOfPlayers - 1].position, spawnPoint[PhotonNetwork.CountOfPlayers - 1].rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator SpawnPlayer()

    {

        //yield return new WaitUntil(PhotonNetworkIsConnectedAndReady);
        yield return new WaitUntil(() => PhotonNetwork.InRoom);


        if (PhotonNetwork.IsConnected)
        {
            int spawnIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;

            PhotonNetwork.Instantiate(
                character.name,
                spawnPoint[spawnIndex].position,
                spawnPoint[spawnIndex].rotation,
                0
            );


        }



    }

    private bool PhotonNetworkIsConnectedAndReady()

    {

        return PhotonNetwork.IsConnectedAndReady;

    }
}
