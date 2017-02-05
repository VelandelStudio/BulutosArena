using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class TargetHUD : NetworkBehaviour {

    [SerializeField]
    private Image reticule;
    [SerializeField]
    private Image viseur;
    [SerializeField]
    private Image lineTarget;
    [SerializeField]
    private Text textTarget;

    private const string PLAYER_TAG = "Player";

    void Start () {
        SetDefaultValues();
    }

    public void targetDetected(Collider target)
    {

        if (target != null) { 
            if (target.tag != PLAYER_TAG)
            {
                Debug.Log(textTarget.text);
                textTarget.enabled = true;
                textTarget.color = Color.green;
                textTarget.text = "Target : " + target.name +"\n";

                lineTarget.enabled = true;
                lineTarget.color = Color.green;

                viseur.color = Color.yellow;
            }
            else
            {
                PlayerManager playerTarget = target.gameObject.GetComponent<PlayerManager>();

                textTarget.enabled = true;
                textTarget.color = Color.red;

                textTarget.text = "Target : " + target.name + "\n";
                textTarget.text += "Position : " + target.transform.position + "\n";
                textTarget.text += "Health : " + playerTarget.getHealth();

                lineTarget.enabled = true;
                lineTarget.color = Color.red;

                viseur.color = Color.red;
            }
        }
    }

    public void noTargetDetected()
    {
        SetDefaultValues();
    }

    private void SetDefaultValues()
    {
        reticule.enabled = true;
        viseur.enabled = true;
        lineTarget.enabled = false;
        textTarget.enabled = false;

        textTarget.text = "";
        reticule.color = Color.blue;
        viseur.color = Color.blue;
        lineTarget.color = Color.white;
    }
}
