using Photon.Pun;
using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    NavMeshAgent agent;
    FirstPersonController target;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
            agent.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!PhotonNetwork.IsMasterClient) return;

        FindNearestPlayer();
        if (target == null) return;

        agent.SetDestination(target.transform.position);
    }

    void FindNearestPlayer()
    {
        var players = FindObjectsByType<FirstPersonController>(FindObjectsSortMode.None);

        float minDist = Mathf.Infinity;
        FirstPersonController nearest = null;

        foreach (var p in players)
        {

            float d = Vector3.Distance(transform.position, p.transform.position);
            if (d < minDist)
            {
                minDist = d;
                nearest = p;
            }
        }

        target = nearest;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!PhotonNetwork.IsMasterClient) return;

        if (other.CompareTag("Player"))
        {
            //GetComponent<EnemyHealth>()?.SelfDestruct();
        }
    }
}
