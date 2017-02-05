using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponBase : NetworkBehaviour
{
    private Transform gunEnd;
    public int gunDamage = 0;
    public float fireRate = 0f;
    public float weaponRange = 0f;
    public float hitForce = 0f;
    public string weaponName;
}
