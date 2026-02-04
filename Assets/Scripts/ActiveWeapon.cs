using Cinemachine;
using Photon.Pun;
using StarterAssets;
using System.Collections;
using TMPro;
using UnityEngine;

public class ActiveWeapon : MonoBehaviourPun
{
    [SerializeField] PlayerController playerController;
    [SerializeField] WeaponSO[] allWeapons;
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
        if (!photonView.IsMine) return;

    }

    private void Update()
    {
        if (!photonView.IsMine) return;

        if (currentWeapon == null)
        {
            return;
        }

        Test();
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


    public void EquipWeaponLocal(int weaponId)
    {
        if (currentWeapon)
            Destroy(currentWeapon.gameObject);

        WeaponSO weaponSO = allWeapons[weaponId];

        Weapon weapon = Instantiate(weaponSO.weaponPrefab, transform)
            .GetComponent<Weapon>();

        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;

        currentWeapon = weapon;
        currentWeaponSO = weaponSO;
        //AdjustAmmo(currentWeaponSO.MagazineSize);

    }

    void Test()
    {
        if (!starterAssetsInputs.shoot) return;
        playerController.RequestFire();
    }


    public void HandleShoot()
    {
        timeToNextShot += Time.deltaTime;
        if (timeToNextShot < currentWeaponSO.FireRate)
        {
            return;

        }
        timeToNextShot = 0f;
        //AdjustAmmo(-1);
        currentWeapon.Shoot(currentWeaponSO);

        if (!currentWeaponSO.IsAutomatic)
        {

            starterAssetsInputs.ShootInput(false);
        }
    }

    public void PlayFireEffectsOnly()
    {
        if (!currentWeapon) return;
        currentWeapon.PlayEffect();
    }

}
