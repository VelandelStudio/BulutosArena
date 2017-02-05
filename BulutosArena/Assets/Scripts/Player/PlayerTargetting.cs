using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerTargetting : NetworkBehaviour {

    public PlayerWeapon weapon;

    [SerializeField]
    private Camera cameraPlayer;

    [SerializeField]
    private LayerMask layerMask;

    private const string PLAYER_TAG = "Player";
    private TargetHUD targetHUD;

    private void Start()
    {
        if (cameraPlayer == null)
        {
            Debug.LogError("PlayerShoot : Camera absente !");
            this.enabled = false;
        }

        GameObject objHUD = GameObject.FindGameObjectWithTag("TargetHUD");
        targetHUD = objHUD.GetComponent< TargetHUD>();
    }

    private void Update()
    {
        CmdTarget();
    }

    [Client]
    private void CmdTarget()
    {
        RaycastHit hit;
        Debug.DrawRay(cameraPlayer.transform.position, cameraPlayer.transform.forward * weapon.range, Color.red);
        if (Physics.Raycast(cameraPlayer.transform.position, cameraPlayer.transform.forward, out hit, weapon.range, layerMask))
            targetHUD.targetDetected(hit.collider);
        else
            targetHUD.noTargetDetected();
    }
}
