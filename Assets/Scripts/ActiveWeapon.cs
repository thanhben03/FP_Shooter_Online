using Cinemachine;
using Photon.Pun;
using StarterAssets;
using System.Collections;
using System.Linq;
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


    public void EquipWeaponLocal(int weaponId)
    {
        if (currentWeapon)
            Destroy(currentWeapon.gameObject);
        
        WeaponSO weaponSO = GetWeaponSOFromID(weaponId);

        Weapon weapon = Instantiate(weaponSO.weaponPrefab, transform)
            .GetComponent<Weapon>();

        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;

        currentWeapon = weapon;
        currentWeaponSO = weaponSO;
        //AdjustAmmo(currentWeaponSO.MagazineSize);

    }

    public void EquipWeaponNetwork(int weaponId)
    {
        if (!photonView.IsMine) return;
        photonView.RPC(nameof(RPC_EquipWeapon), RpcTarget.AllBuffered, weaponId);
    }

    [PunRPC]
    void RPC_EquipWeapon(int weaponId)
    {
        EquipWeaponLocal(weaponId);
    }

    private WeaponSO GetWeaponSOFromID(int weaponId)
    {
        for (int i = 0; i < allWeapons.Length; i++)
        {
            if (allWeapons[i].ID == weaponId)
            {
                return allWeapons[i];
            }
        }

        return null;
    }

    void Test()
    {
    }


    public void HandleShoot()
    {
        timeToNextShot += Time.deltaTime;
        if (timeToNextShot < currentWeaponSO.FireRate)
        {
            return;

        }
        timeToNextShot = 0f;
        if (!starterAssetsInputs.shoot) return;
        Debug.Log("Fireee");

        playerController.RequestFire();

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
