using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerManager))]
public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    private Behaviour[] componentsToDisable;

    [SerializeField]
    private string remoteLayerName = "RemotePlayerLayer";

    private Camera sceneCamera;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
        }
        else
        {
            DisableSceneCamera();
        }

        GetComponent<PlayerManager>().Setup();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        string netID = GetComponent<NetworkIdentity>().netId.ToString();
        PlayerManager player = GetComponent<PlayerManager>();
        GameManager.RegisterPlayer(netID, player);
    }

    private void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    private void DisableComponents()
    {
        foreach (Behaviour component in componentsToDisable)
            component.enabled = false;
    }

    private void DisableSceneCamera()
    {
        sceneCamera = Camera.main;
        if (sceneCamera != null)
            sceneCamera.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if(sceneCamera != null)
            sceneCamera.gameObject.SetActive(true);

        GameManager.UnregisterPlayer(transform.name);
    }
}
