using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot: NetworkBehaviour {

    public PlayerWeapon weapon;

    [SerializeField]
    private Camera cameraPlayer;

    [SerializeField]
    private LayerMask layerMask;

    private const string PLAYER_TAG = "Player";
    private void Start()
    {
        if (cameraPlayer == null)
        {
            Debug.LogError("PlayerShoot : Camera absente !");
            this.enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown ("Fire1"))
        {
            Shoot();
        }
    }

    [Client]
    private void Shoot()
    {
        RaycastHit hit;
        Debug.DrawRay(cameraPlayer.transform.position, cameraPlayer.transform.forward * weapon.range, Color.red);
        if(Physics.Raycast(cameraPlayer.transform.position, cameraPlayer.transform.forward, out hit, weapon.range, layerMask))
        {
            Debug.Log("Je tire sur : " + hit.collider.name);
            if (hit.collider.tag == PLAYER_TAG)
                CmdPlayerShot(hit.collider.name);
        }
    }

    [Command]
    private void CmdPlayerShot(string ID)
    {
        Debug.Log(ID + " has been shot");
    }
}
