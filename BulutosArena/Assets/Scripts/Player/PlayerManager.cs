using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerManager : NetworkBehaviour {

    [SerializeField]
    private int maxHealth = 100;

    [SyncVar]
    private int currentHealth;

    [SyncVar]
    private bool isDead = false;
    public bool isDeadStruct
    {
        get { return isDead; }
        protected set { isDead = value; }
    }

    [SerializeField]
    private Behaviour[] disableOnDeath;

    private bool[] wasEnabled;

    public void Setup()
    {
        wasEnabled = new bool[disableOnDeath.Length];

        for (int i = 0; i < wasEnabled.Length; i++)
            wasEnabled[i] = disableOnDeath[i].enabled;

        setDefaults();
    }

    [ClientRpc]
    public void RpcTakeDamage(int amount)
    {
        if (isDead)
            return;

        currentHealth -= amount;
        Debug.Log(transform.name + "now has " + currentHealth + " health.");
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        for (int i = 0; i < disableOnDeath.Length; i++)
            disableOnDeath[i].enabled = false;

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

        Debug.Log(transform.name + " is isDead !");
        StartCoroutine(Respawn());
        
    }

    public void setDefaults()
    {
        isDead = false;
        currentHealth = maxHealth;

        for (int i = 0; i < disableOnDeath.Length; i++)
            disableOnDeath[i].enabled = wasEnabled[i];

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = true;
    }   

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.gameManagerInstance.matchSettings.RespawnTime);

        setDefaults();
        Transform spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;

        Debug.Log(transform.name + " respawned !");
    }

    public int getHealth()
    {
        return currentHealth;
    }
}
