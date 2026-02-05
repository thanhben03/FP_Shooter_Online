using Photon.Pun;
using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviourPun 
{
    [SerializeField] int health;
   
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
        //if (!photonView.IsMine) return;

        health -= damage;
        HealthUI.Instance.UpdateUIHealth(health);

    }
}
