using System.Collections;
using UnityEngine.Networking;
using UnityEngine;

public class PlayerShooting : NetworkBehaviour {

    bool canShoot;
    float ellapsedTime;

    //[SerializeField]
    Transform gunEnd;
    [SerializeField]
    int gunDamage;
    [SerializeField]
    float fireRate;
    [SerializeField]
    float weaponRange;
    [SerializeField]
    float hitForce;

    public virtual void Start()
    {
        if (isLocalPlayer)
        {
            canShoot = true;
            Transform[] gunEnds = gameObject.GetComponentsInChildren<Transform>(true);
            foreach (Transform gunEndElement in gunEnds)
            {
                if (gunEndElement.CompareTag("GunEnd")) {
                    gunEnd = gunEndElement;
                }
            }
        }
    }

    public virtual void Update()
    {
        if (!canShoot)
            return;
        ellapsedTime += Time.deltaTime;

        if(Input.GetButton("Fire1") && ellapsedTime > fireRate)
        {
            ellapsedTime = 0f;
            CmdFireShot(gunEnd.position, gunEnd.forward);
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
                //TakeDamageScripts;
            }

        }

        RpcProcessShotEffects(playerImpact, hit.point);
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
}
