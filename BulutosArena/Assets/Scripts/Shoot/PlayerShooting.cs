using System.Collections;
using UnityEngine.Networking;
using UnityEngine;

public class PlayerShooting : NetworkBehaviour {

    bool canShoot;
    float ellapsedTime;

    private Transform gunEnd;
    private int gunDamage;
    private float fireRate;
    private float weaponRange;
    private float hitForce;
    private string weaponName;

    private WeaponBase weapon;
    private WeaponBase[] Allweapons;

    private void Start()
    {
        if (isLocalPlayer)
        {
            canShoot = true;
            weapon = gameObject.GetComponentInChildren<WeaponBase>();
            Allweapons = gameObject.GetComponentsInChildren<WeaponBase>(true);
            getGunEnd();
            UpdateWeapon();
        } 
    }

    private void Update()
    {
        if (!canShoot)
            return;

        ellapsedTime += Time.deltaTime;

        if(Input.GetButton("Fire1") && ellapsedTime > fireRate && gunEnd!= null)
        {
            ellapsedTime = 0f;
            CmdFireShot(gunEnd.position, gunEnd.forward);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            CmdSwitchWeapon();
        }

            weapon = gameObject.GetComponentInChildren<WeaponBase>();
        if (weapon != null && weaponName != weapon.weaponName)
        {
            getGunEnd();
            UpdateWeapon();
        }
    }

    [Command]
    void CmdFireShot(Vector3 origin, Vector3 direction)
    {
        RaycastHit hit;
        Ray ray = new Ray(origin, direction);
        Debug.DrawRay(ray.origin,ray.direction, Color.red, 1f);

        bool playerImpact = Physics.Raycast(ray, out hit, weaponRange);
        if (playerImpact)
        {
            if (hit.collider.tag == "Player")
            {
                PlayerHealth enemy = hit.transform.GetComponent<PlayerHealth>();
                if (enemy != null)
                    enemy.TakeDamage();
            }

        }

        RpcProcessShotEffects(playerImpact, hit.point);
    }

    [Command]
    void CmdSwitchWeapon()
    {
        weapon.gameObject.SetActive(false);
        foreach (WeaponBase newWeapon in Allweapons)
        {
            if (newWeapon.gameObject.name == "Mitraillette")
                newWeapon.gameObject.SetActive(true);

            Debug.Log(newWeapon.weaponName);
        }
    }

    [ClientRpc]
    void RpcProcessShotEffects(bool playerImpact, Vector3 point)
    {
        //play process sounds + lumieres

        if (playerImpact)
        {
            // play process impacts
        }
    }

    private void UpdateWeapon()
    {
        weaponName = weapon.weaponName;
        gunDamage = weapon.gunDamage;
        fireRate = weapon.fireRate;
        weaponRange = weapon.weaponRange;
        hitForce = weapon.hitForce;
    }

    private void getGunEnd()
    {
        Transform[] gunEnds = weapon.gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform gunEndElement in gunEnds)
            if (gunEndElement.CompareTag("GunEnd"))
                gunEnd = gunEndElement;
    }
}
