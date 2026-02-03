using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    PhotonView photonView;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        photonView = GetComponentInParent<PhotonView>();
        if (!photonView.IsMine)
        {
            return;
        }

        CinemachineVirtualCamera vcam =
            FindAnyObjectByType<CinemachineVirtualCamera>();

        vcam.Follow = transform;
        vcam.LookAt = transform;
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
