using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float moveSpeed;
    public float sensitive = 2f;
    public Camera eyes;

    float rotateX;
    float rotateY;

    void Start () {
		
	}
	
	void Update () {
        MovePlayer();
        RotateCamera();
        
	}

    private void MovePlayer()
    {
        transform.position += transform.forward * Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;
        transform.position += transform.right * Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;

        //transform.position += transform.up * Input.GetAxisRaw("") * moveSpeed * Time.deltaTime;
    }

    private void RotateCamera()
    {


        rotateX = Input.GetAxis("Mouse X") * sensitive;
        rotateY = Input.GetAxis("Mouse Y") * sensitive;

        transform.Rotate(0, rotateX, 0);
        eyes.transform.Rotate(-rotateY, 0, 0);
    }
}
