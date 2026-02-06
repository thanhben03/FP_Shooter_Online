using Photon.Pun;
using System;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int spawnIndex;
    public int poolId;
    public int ammoAmount = 30;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PhotonView playerPV = other.GetComponent<PhotonView>();


            if (playerPV.IsMine)
            {
                PlayerController playerController = other.GetComponent<PlayerController>();
                if (playerController != null && playerController.activeWeapon)
                {
                    playerController.AddAmmo();
                    AmmoSpawner.Instance.RequestPickup(poolId, playerPV.ViewID);

                }
            }

        }
    }

}
