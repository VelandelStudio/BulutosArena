using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(WeaponManager))]
[RequireComponent(typeof(PlayerSetup))]
public class PlayerShoot: NetworkBehaviour {

    [SerializeField]
    private Camera cameraPlayer;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private PlayerSetup playerSetup;

    private const string PLAYER_TAG = "Player";
    private TargetHUD targetHUD;
    private PlayerWeapon currentWeapon;
    private WeaponManager weaponManager;

    private void Start()
    {
        if (cameraPlayer == null)
        {
            Debug.LogError("PlayerShoot : Camera absente !");
            this.enabled = false;
        }
        weaponManager = GetComponent<WeaponManager>();

        if(isLocalPlayer)
            targetHUD = playerSetup.GetHUDInstance().GetComponent<TargetHUD>();
    }

    private void Update()
    {
        currentWeapon = weaponManager.GetPlayerWeapon();
        CmdTargetting();

        if (currentWeapon.fireRate <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
                CmdShoot();
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
                InvokeRepeating("CmdShoot", 0f, 1f/currentWeapon.fireRate);
            else if (Input.GetButtonUp("Fire1"))
            {
                CancelInvoke("CmdShoot");
            }
        }
    }

    [Command]
    private void CmdOnShoot()
    {
        RpcDoShootEffects();
    }

    [Command]
    private void CmdOnHit(Vector3 pos, Vector3 normalVector)
    {
        RpcDoHitEffects(pos, normalVector);
    }

    [ClientRpc]
    private void RpcDoShootEffects()
    {
        weaponManager.GetPlayerWeaponGraphics().muzzleFlash.Stop();
        weaponManager.GetPlayerWeaponGraphics().muzzleFlash.Play();
    }

    [ClientRpc]
    private void RpcDoHitEffects(Vector3 pos, Vector3 normalVector)
    {
        GameObject hitEffect = Instantiate(weaponManager.GetPlayerWeaponGraphics().impactEffectPrefab, pos, Quaternion.LookRotation(normalVector));
        Destroy(hitEffect, 2f);
    }

    [Client]
    private void CmdTargetting()
    {
        if (!isLocalPlayer)
            return;

        if (targetHUD != null)
        {
            RaycastHit hit;
            Physics.Raycast(cameraPlayer.transform.position, cameraPlayer.transform.forward, out hit, currentWeapon.range, layerMask);
            targetHUD.DisplayTargetInfo(hit.collider);
        }
    }

    [Command]
    private void CmdShoot()
    {
        if (!isLocalPlayer)
            return;

        CmdOnShoot();

        RaycastHit hit;
        if(Physics.Raycast(cameraPlayer.transform.position, cameraPlayer.transform.forward, out hit, currentWeapon.range, layerMask))
        {
            if (hit.collider.tag == PLAYER_TAG)
                CmdPlayerShot(hit.collider.name, currentWeapon.damage);

            CmdOnHit(hit.point, hit.normal);
        }
    }

    [Command]
    private void CmdPlayerShot(string playerID, int weaponDamage)
    {
        PlayerManager player = GameManager.GetPlayer(playerID);
        player.RpcTakeDamage(weaponDamage);
    }
}