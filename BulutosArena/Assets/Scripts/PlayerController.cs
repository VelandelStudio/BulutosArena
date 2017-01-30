using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text winText;
    private Rigidbody rigidBody;
    private int count;


    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        count = 0;
        setCountText();
        winText.text = "";
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rigidBody.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
            count++;
            setCountText();
        }
    }

    void setCountText()
    {
        countText.text = "Count : " + count.ToString();
        if (count >= 20)
            winText.text = "AMAZING YOU COLLECTED THE DICK !";
    }
}
