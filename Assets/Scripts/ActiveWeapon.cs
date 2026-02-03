using Cinemachine;
using Photon.Pun;
using StarterAssets;
using TMPro;
using UnityEngine;

public class ActiveWeapon : MonoBehaviourPun
{

    [SerializeField] WeaponSO currentWeaponSO;
    [SerializeField] Animator animator;
    [SerializeField] private CinemachineVirtualCamera playerFollowCamera;
    [SerializeField] private GameObject zoomInImage;
    [SerializeField] Camera weaponCamera;

    public TextMeshProUGUI ammoText;
    public WeaponSO WeaponSO => currentWeaponSO;

    StarterAssetsInputs starterAssetsInputs;
    [SerializeField] Weapon currentWeapon;
    float defaultFOV;
    int currentAmmo;



    [SerializeField] private float timeToNextShot = 0f;

    private void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        //defaultFOV = playerFollowCamera.m_Lens.FieldOfView;
        //if (!photonView.IsMine) return;

        SwitchWeapon(currentWeaponSO);
    }

    private void Update()
    {
        if (!photonView.IsMine) return;

        if (currentWeapon == null)
        {
            return;
        }

        HandleShoot();
        //HandleZoom();
    }

    public void AdjustAmmo(int amount)
    {
        currentAmmo += amount;
        if (currentAmmo > currentWeaponSO.MagazineSize)
        {
            currentAmmo = currentWeaponSO.MagazineSize;
        }
        ammoText.text = currentAmmo.ToString();
    }

    public void SwitchWeapon(WeaponSO weaponSO)
    {
        if (currentWeapon)
        {
            PhotonNetwork.Destroy(currentWeapon.gameObject);
        }
        Weapon weapon = PhotonNetwork.Instantiate(
            weaponSO.weaponPrefab.name,
            Vector3.zero, Quaternion.identity
        ).GetComponent<Weapon>();

        weapon.transform.SetParent(transform, true);

        currentWeapon = weapon;
        currentWeaponSO = weaponSO;
        //AdjustAmmo(currentWeaponSO.MagazineSize);

    }

    private void HandleShoot()
    {
        timeToNextShot += Time.deltaTime;
        if (timeToNextShot < currentWeaponSO.FireRate || currentAmmo <= 0)
        {
            return;

        }
        if (!starterAssetsInputs.shoot) return;
        timeToNextShot = 0f;
        //AdjustAmmo(-1);
        currentWeapon.Shoot(currentWeaponSO);
        animator.Play("PistolShot", 0, 0f);

        if (!currentWeaponSO.IsAutomatic)
        {

            starterAssetsInputs.ShootInput(false);
        }
    }

    //void HandleZoom()
    //{
    //    if (!currentWeaponSO.CanZoom)
    //    {
    //        return;
    //    }

    //    if (starterAssetsInputs.zoom)
    //    {
    //        zoomInImage.SetActive(true);
    //        playerFollowCamera.m_Lens.FieldOfView = Mathf.Lerp(playerFollowCamera.m_Lens.FieldOfView, defaultFOV - currentWeaponSO.ZoomAmount, Time.deltaTime * 10f);
    //        weaponCamera.fieldOfView = currentWeaponSO.ZoomAmount - 10f;
    //    }
    //    else
    //    {
    //        zoomInImage.SetActive(false);
    //        playerFollowCamera.m_Lens.FieldOfView = Mathf.Lerp(playerFollowCamera.m_Lens.FieldOfView, defaultFOV, Time.deltaTime * 10f);
    //        weaponCamera.fieldOfView = defaultFOV;

    //    }
    //}
}
