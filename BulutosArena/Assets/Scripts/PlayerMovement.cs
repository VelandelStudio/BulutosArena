using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float moveSpeed;
    public float jumpSpeed;
    private float initialSpeed;
    private bool isJumping = false;
    private Rigidbody rb;

    public float sensitive = 2f;
    public Camera eyes;

    float rotateX;
    float rotateY;

    void Start () {
        initialSpeed = moveSpeed;
        rb = GetComponent<Rigidbody>();
    }
	
	void FixedUpdate () {
        MovePlayer();
        JumpHandler();
        RotateCamera();
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

    private void RotateCamera()
    {
        rotateX = Input.GetAxis("Mouse X") * sensitive;
        rotateY = Input.GetAxis("Mouse Y") * sensitive;

        eyes.transform.Rotate(-rotateY, 0, 0);
        transform.Rotate(0, rotateX, 0);
    }
}
