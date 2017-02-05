using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot: NetworkBehaviour {

    public PlayerWeapon weapon;

    [SerializeField]
    private Camera cameraPlayer;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private PlayerSetup playerSetup;

    private const string PLAYER_TAG = "Player";
    private TargetHUD targetHUD;

    private void Start()
    {
        targetHUD = playerSetup.GetHUDInstance().GetComponent<TargetHUD>();
        if (cameraPlayer == null)
        {
            Debug.LogError("PlayerShoot : Camera absente !");
            this.enabled = false;
        }
    }

    private void Update()
    {
        Targetting();

        if (Input.GetButtonDown ("Fire1"))
        {
            Shoot();
        }
    }

    [Client]
    private void Targetting()
    {
        if (targetHUD != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(cameraPlayer.transform.position, cameraPlayer.transform.forward, out hit, weapon.range, layerMask))
                targetHUD.targetDetected(hit.collider);
            else
                targetHUD.noTargetDetected();
        }
    }


    [Client]
    private void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(cameraPlayer.transform.position, cameraPlayer.transform.forward, out hit, weapon.range, layerMask))
        {
            Debug.Log("Je tire sur : " + hit.collider.name);
            if (hit.collider.tag == PLAYER_TAG)
                CmdPlayerShot(hit.collider.name, weapon.damage);
        }
    }

    [Command]
    private void CmdPlayerShot(string playerID, int weaponDamage)
    {
        PlayerManager player = GameManager.GetPlayer(playerID);
        player.RpcTakeDamage(weaponDamage);
    }
}
