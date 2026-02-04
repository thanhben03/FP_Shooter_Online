using Photon.Pun;
using StarterAssets;
using UnityEngine;
using UnityEngine.ProBuilder;

public class Turret : MonoBehaviour
{
    [SerializeField] FirstPersonController target;
    [SerializeField] Transform turretHead;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        FindNearestPlayer();
        if (target == null) return;
        turretHead.LookAt(target.gameObject.transform);
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
}
