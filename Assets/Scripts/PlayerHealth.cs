using Photon.Pun;
using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviourPun 
{
    [SerializeField] int health;
    public event Action OnLoseGame;
    public PlayerCamera playerCamera;
    void Start()
    {
        health = 5;
        //if (!photonView.IsMine) return;
        HealthUI.Instance.UpdateUIHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        //if (!photonView.IsMine) return;s
        health -= damage;
        HealthUI.Instance.UpdateUIHealth(health);
        if (health <= 0)
        {
            // neu khong con player nao song
            //if (PhotonNetwork.PlayerList.Length <= 1)
            //{
            //    photonView.RPC(nameof(RPC_GameLose), RpcTarget.All);
            //} else
            //{

            //}
            if (!GameManager.Instance.IsDiedAll())
            {
                playerCamera.SwitchCamera();
                PhotonNetwork.Destroy(gameObject);
                GameObject.FindGameObjectWithTag("Canvas").SetActive(false);

            } else
            {
                photonView.RPC(nameof(RPC_GameLose), RpcTarget.All);
            }
        }
    }

    [PunRPC]
    void RPC_GameLose()
    {
        GameManager.Instance.RPC_GameLose();
    }
}
