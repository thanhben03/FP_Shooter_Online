using Photon.Pun;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 30f;
    [SerializeField] GameObject projectileVFXPrefab;
    Rigidbody rb;
    int damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Init(int amount)
    {
        damage = amount;
    }

    private void Start()
    {
        rb.linearVelocity = transform.forward * speed;
    }


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            PhotonView playerPV = other.GetComponent<PhotonView>();
            Instantiate(projectileVFXPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            if (!playerPV.IsMine) return;
            Debug.Log(playerPV.ViewID);

        }
    }
}
