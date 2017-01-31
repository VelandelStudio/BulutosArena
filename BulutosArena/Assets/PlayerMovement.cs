using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float moveSpeed;

	void Start () {
		
	}
	
	void Update () {
        MovePlayer();
	}

    private void MovePlayer()
    {
        transform.position += transform.forward * Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;
        transform.position += transform.right * Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        //transform.position += transform.up * Input.GetAxisRaw("") * moveSpeed * Time.deltaTime;
    }
}
