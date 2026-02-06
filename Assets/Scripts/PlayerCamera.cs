using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    PhotonView photonView;
    CinemachineVirtualCamera vcam;
    [SerializeField] ActiveWeapon activeWeapon;
    [SerializeField] Camera weaponCamera;
    float defaultFOV;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        activeWeapon.OnZoomIn += ActiveWeapon_OnZoomIn;
        photonView = GetComponentInParent<PhotonView>();
        if (!photonView.IsMine)
        {
            return;
        }

        vcam =
            GameObject.Find("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>();

        vcam.Follow = transform;
        vcam.LookAt = transform;
        defaultFOV = vcam.m_Lens.FieldOfView;
    }

    private void ActiveWeapon_OnZoomIn(bool isZoom, float zoomAmoun)
    {
        if (isZoom)
        {
            vcam.m_Lens.FieldOfView = Mathf.Lerp(vcam.m_Lens.FieldOfView, defaultFOV - zoomAmoun, Time.deltaTime * 10f);
            weaponCamera.fieldOfView = zoomAmoun - 10f;
        } else
        {
            vcam.m_Lens.FieldOfView = Mathf.Lerp(vcam.m_Lens.FieldOfView, defaultFOV, Time.deltaTime * 10f);
            weaponCamera.fieldOfView = defaultFOV;
        }

        ZoomInUI.Instance.UpdateUI(isZoom);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchCamera()
    {
        PlayerCamera playerCamera = FindOtherPlayerCamera();
        if (playerCamera != null)
        {
            vcam.Follow = playerCamera.transform;
            vcam.LookAt = playerCamera.transform;
        }
    }

    public PlayerCamera FindOtherPlayerCamera()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        GameObject firstOther = null;

        foreach (GameObject p in players)
        {
            PhotonView pv = p.GetComponent<PhotonView>();

            if (pv != null && pv.IsMine)
                continue;

            firstOther = p;
            break;
        }

        return firstOther.GetComponentInChildren<PlayerCamera>();
    }
}
