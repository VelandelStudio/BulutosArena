using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float moveSpeed;
    public float jumpSpeed;

    private float initialSpeed;
    private bool isJumping = false;
    private float initialPositionY;
    private Rigidbody rb;

    void Start () {
        initialSpeed = moveSpeed;
        rb = GetComponent<Rigidbody>();
    }
	
	void Update () {
        MovePlayer();
        JumpHandler();
    }

    private void MovePlayer()
    {
        if (Input.GetButton("Sprint"))
            moveSpeed = initialSpeed * 2f;
        else
            moveSpeed = initialSpeed;

        transform.position += transform.forward * Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;
        transform.position += transform.right * Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
    }

    private void JumpHandler()
    {
        if (!isJumping && Input.GetButton("Jump"))
        {
            rb.AddForce(Vector3.up * 250f, ForceMode.Impulse);
            isJumping = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Floor")
            isJumping = false;
    } 

    /*private IEnumerator JumpCycle()
    {
        transform.position += transform.up * jumpSpeed * Time.deltaTime;
        return;
    }*/
}
