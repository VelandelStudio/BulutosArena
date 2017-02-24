using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[RequireComponent(typeof(PlayerSetup))]
public class PlayerManager : NetworkBehaviour {

    [SerializeField]
    private GameObject deathEffects;
    [SerializeField]
    private GameObject spawnEffects;

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
    [SerializeField]
    private GameObject[] disableGameObjectsOnDeath;

    [SerializeField]
    private PlayerSetup player;

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

        for (int i = 0; i < disableGameObjectsOnDeath.Length; i++)
            disableGameObjectsOnDeath[i].SetActive(false);

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

        if (isLocalPlayer)
        {
            player.GetHUDInstance().GetComponent<TargetHUD>().ManageUIOnDeath(isDead);
            GameManager.gameManagerInstance.SetSceneCameraActive(true);
        }

        GameObject explosionInstance = Instantiate(deathEffects, transform.position, Quaternion.identity);
        Destroy(explosionInstance, 5f);

        StartCoroutine(Respawn());
        
    }

    public void setDefaults()
    {
        isDead = false;
        currentHealth = maxHealth;

        for (int i = 0; i < disableOnDeath.Length; i++)
            disableOnDeath[i].enabled = wasEnabled[i];

        for (int i = 0; i < disableGameObjectsOnDeath.Length; i++)
            disableGameObjectsOnDeath[i].SetActive(true);

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = true;

        if (isLocalPlayer)
        {
            player.GetHUDInstance().GetComponent<TargetHUD>().ManageUIOnDeath(isDead);
            GameManager.gameManagerInstance.SetSceneCameraActive(false);
        }

        GameObject spawnInstance = Instantiate(spawnEffects, transform.position, Quaternion.identity);
        Destroy(spawnInstance, 3f);
    }   

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.gameManagerInstance.matchSettings.RespawnTime);

        Transform spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
        setDefaults();
    }

    public int getHealth()
    {
        return currentHealth;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.K))
            RpcTakeDamage(99999);
    }
}
