using UnityEngine;
using UnityEngine.Networking;

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

        RegisterPlayer();
    }

    private void RegisterPlayer()
    {
        string ID = "Player_" + GetComponent<NetworkIdentity>().netId;
        transform.name = ID;
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
    }
}
