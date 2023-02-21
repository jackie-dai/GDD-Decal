using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovement : MonoBehaviour {
    Rigidbody2D playerRigidbody;
    [SerializeField]
    private float movementSpeed = 5f;
    float xAxis;
    float yAxis;

	// Use this for initialization
	void Start () {
        playerRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");

        moveFunction3();

	}

    void moveFunction1() {
        Vector2 movementVector = new Vector2(xAxis, yAxis);
        playerRigidbody.AddForce(movementVector);
    }

    void moveFunction2() {
        Vector2 movementVector = new Vector2(xAxis, yAxis);
        playerRigidbody.MovePosition(playerRigidbody.position + movementVector * Time.deltaTime);
    }

    void moveFunction3() {
        Vector2 movementVector = new Vector2(xAxis, yAxis);
        playerRigidbody.velocity = movementVector * movementSpeed;
    }
}
