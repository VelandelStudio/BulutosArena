using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour {

    [SerializeField] int maxHealth = 3;
    PlayerBaseScript player;
    int currentHealth;

    void Start()
    {
        if(isLocalPlayer)
            player = GetComponent<PlayerBaseScript>();
    }

    [ServerCallback]
    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    [Server]
    public bool TakeDamage()
    {
        bool died = false;

        if(currentHealth <= 0)
            return died;

        currentHealth--;
        died = currentHealth <= 0;

        RpcTakeDamage(died);

        return died;
    }

    [ClientRpc]
    private void RpcTakeDamage(bool died)
    {
        if (died)
            player.Die();
    }
}
