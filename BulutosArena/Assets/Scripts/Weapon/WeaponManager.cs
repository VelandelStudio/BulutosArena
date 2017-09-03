using UnityEngine;
using UnityEngine.Networking;

public class WeaponManager : NetworkBehaviour
{
    [SerializeField]
    private string weaponLayerName = "WeaponLayer";
    [SerializeField]
    private Transform weaponHolder;
    [SerializeField]
    private PlayerWeapon weaponBase;

    private PlayerWeapon currentWeapon;
    private WeaponGraphics currentWeaponGraphics;

    private void Start()
    {
        EquipeWeapon(weaponBase);
    }

    private void EquipeWeapon(PlayerWeapon newWeapon)
    {
        currentWeapon = newWeapon;
        GameObject weaponInstance = (GameObject) Instantiate(newWeapon.graphics, weaponHolder.position, weaponHolder.rotation);
        weaponInstance.transform.SetParent(weaponHolder);

        currentWeaponGraphics = weaponInstance.GetComponent<WeaponGraphics>();
        if (currentWeaponGraphics == null)
            Debug.LogError("No Graphics component in the weapon object : " + weaponInstance.name);

        if (isLocalPlayer)
            Utils.SetLayerRecursively(weaponInstance, LayerMask.NameToLayer(weaponLayerName));
    }

    public PlayerWeapon GetPlayerWeapon()
    {
        return currentWeapon;
    }

    public WeaponGraphics GetPlayerWeaponGraphics()
    {
        return currentWeaponGraphics;
    }
}
