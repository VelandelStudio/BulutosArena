using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerManager))]
public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    private Behaviour[] componentsToDisable;

    [SerializeField]
    private string remoteLayerName = "RemotePlayerLayer";

    [SerializeField]
    private string dontRenderOnCamLayer = "DontRenderOnCam";

    [SerializeField]
    private GameObject playerGraphics;

    [SerializeField]
    private GameObject playerUIPrefab;
    private GameObject playerUIInstance;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
        }
        else
        {
            SetLayerRecursively(playerGraphics, LayerMask.NameToLayer(dontRenderOnCamLayer));

            playerUIInstance = Instantiate(playerUIPrefab);
            playerUIInstance.name = playerUIPrefab.name;
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

    public GameObject GetHUDInstance()
    {
        return playerUIInstance;
    }

    private void SetLayerRecursively(GameObject graphics, int newLayer)
    {
        graphics.layer = newLayer;
        foreach (Transform child in graphics.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
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

    private void OnDisable()
    {
        Destroy(playerUIInstance);

        GameManager.gameManagerInstance.SetSceneCameraActive(true);
        GameManager.UnregisterPlayer(transform.name);
    }
}
