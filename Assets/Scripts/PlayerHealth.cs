using Photon.Pun;
using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviourPun 
{
    [SerializeField] int health;
    public event Action<int> OnHealthChange;
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
            playerCamera.SwitchCamera();
            GameObject.FindGameObjectWithTag("Canvas").SetActive(false);

            PhotonNetwork.Destroy(gameObject);
        }
    }
}
