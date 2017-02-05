using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bazooka : WeaponBase
{
    private void Awake()
    {
        gunDamage = 1;
        fireRate = 1f;
        weaponRange = 50f;
        hitForce = 1f;
        weaponName = "Bazooka";
    }
}
