using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWeaponBase : MonoBehaviour {

    public Transform gunEnd;                                            // Holds a reference to the gun end object, marking the muzzle location of the gun
    private Camera fpsCam;  
    private LineRenderer laserLine;                                     // Reference to the LineRenderer component which will display our laserline

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        fpsCam = GetComponentInParent<Camera>();
    }

    public virtual void Shoot(int gunDamage, float fireRate, float weaponRange, float hitForce, WaitForSeconds shotDuration)
    {
        StartCoroutine(ShotEffect(shotDuration));
        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hit;
        laserLine.SetPosition(0, gunEnd.position);

        if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
        {
            laserLine.SetPosition(1, hit.point);

            ShootableBox health = hit.collider.GetComponent<ShootableBox>();
            if (health != null)
            {
                health.Damage(gunDamage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * hitForce);
            }
        }
        else
        {
            laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
        }
    }

    public virtual IEnumerator ShotEffect(WaitForSeconds shotDuration)
    {
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }
}
