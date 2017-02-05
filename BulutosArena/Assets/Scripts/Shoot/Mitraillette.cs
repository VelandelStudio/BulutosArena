using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Mitraillette : WeaponBase
{
    private void Awake()
    {
        gunDamage = 1;
        fireRate = 0.2f;
        weaponRange = 50f;
        hitForce = 1f;
        weaponName = "Mitraillette";
    }
}
