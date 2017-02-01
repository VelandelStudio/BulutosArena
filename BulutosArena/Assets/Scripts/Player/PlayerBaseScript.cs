using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

[System.Serializable]
public class ToggleEvent : UnityEvent<bool> {}

public class PlayerBaseScript : NetworkBehaviour {

    [SerializeField] ToggleEvent onToggleShared;
    [SerializeField] ToggleEvent onToggleLocal;
    [SerializeField] ToggleEvent onToggleRemote;

    private GameObject mainCamera;

    private void Start()
    {
        if (isLocalPlayer)
            mainCamera = GetComponentInChildren<Camera>(true).gameObject;

        EnablePlayer();
    }

    private void DisablePlayer()
    {
        onToggleShared.Invoke(false);

        if (isLocalPlayer)
            onToggleLocal.Invoke(false);
        else
            onToggleRemote.Invoke(false);
    }

    private void EnablePlayer()
    {
        onToggleShared.Invoke(true);

        if(isLocalPlayer)
            onToggleLocal.Invoke(true);
        else
            onToggleRemote.Invoke(true);
    }
}
