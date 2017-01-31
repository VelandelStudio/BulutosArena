using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooka : ShootWeaponBase
{
    public int gunDamage = 1;                                           // Set the number of hitpoints that this gun will take away from shot objects with a health script
    public float fireRate = 0.25f;                                      // Number in seconds which controls how often the player can fire
    public float weaponRange = 50f;                                     // Distance in Unity units over which the player can fire
    public float hitForce = 1000f;                                       // Amount of force which will be added to objects with a rigidbody shot by the player
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);    // WaitForSeconds object used by our ShotEffect coroutine, determines time laser line will remain visible

    private float nextFire;                                             // Float to store the time the player will be allowed to fire again, after firing

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot(gunDamage, fireRate, weaponRange, hitForce, shotDuration);
        }
    }

    public override void Shoot(int gunDamage, float fireRate, float weaponRange, float hitForce, WaitForSeconds shotDuration)
    {
        base.Shoot(gunDamage, fireRate, weaponRange, hitForce, shotDuration);
    }
}
