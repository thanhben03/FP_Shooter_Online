using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PlayerHealth : MonoBehaviourPunCallbacks
{
    [SerializeField] private int health = 5;

    [Header("Refs")]
    [SerializeField] private PlayerCamera playerCamera;

    private bool isDead = false;

    private void Start()
    {
        // chỉ update UI cho player của mình
        if (photonView.IsMine)
        {
            HealthUI.Instance.UpdateUIHealth(health);
        }
    }

    public void TakeDamage(int damage)
    {
        // chỉ player của mình mới được nhận damage
        if (!photonView.IsMine) return;
        if (isDead) return;

        health -= damage;
        HealthUI.Instance.UpdateUIHealth(health);

        if (health <= 0)
        {
            isDead = true;

            // gửi lên master báo: tôi đã chết
            photonView.RPC(nameof(RPC_RequestPlayerDie), RpcTarget.MasterClient, PhotonNetwork.LocalPlayer.ActorNumber);
            HandleLocalDeath();
        }
    }

    private void HandleLocalDeath()
    {
        if (GameManager.Instance.CountPlayerLive() <= 1)
        {
            // gọi thua game cho tất cả
            photonView.RPC(nameof(RPC_GameLose), RpcTarget.All);
        } else
        {
            SwitchCamera();

        }
        UpdateUI();

        //PhotonNetwork.Destroy(gameObject);
    }

    void SwitchCamera()
    {
        if (playerCamera != null)
        {
            playerCamera.SwitchCamera();
        }
    }

    void UpdateUI()
    {
        var canvas = GameObject.FindGameObjectWithTag("Canvas");
        if (canvas != null)
        {
            canvas.SetActive(false);
        }
    }

    // ===================== RPC =====================

    [PunRPC]
    private void RPC_RequestPlayerDie(int actorNumber, PhotonMessageInfo info)
    {
        // chỉ master xử lý
        if (!PhotonNetwork.IsMasterClient) return;

        // update dữ liệu sống/chết ở master
        GameManager.Instance.DecreaseAliveCount();

    }

    [PunRPC]
    private void RPC_GameLose()
    {
        GameManager.Instance.RPC_GameLose();
    }
}
