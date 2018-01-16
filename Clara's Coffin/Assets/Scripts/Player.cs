using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour {

	public float jumpHeight = 4;
	public float timeToJumpApex = 0.4f;
	float acclerationTimeAirborne = 0.2f;
	float acclerationTimeGrounded = 0.1f;
	float moveSpeed = 6;

	float gravity;
	float jumpVelocity;
	Vector3 velocity;
	float velocityXSmooting;

	Controller2D controller;

	// Use this for initialization
	void Start () {
		controller = GetComponent<Controller2D> ();

		gravity = -(2 * jumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs (gravity) * timeToJumpApex;
		print ("Gravity: " + gravity + " jump Velocity: " + jumpVelocity);
	}

	// Update is called once per frame
	void Update () {

		if(controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}

		if(Input.GetKeyDown (KeyCode.Space) && controller.collisions.below) {
			velocity.y = jumpVelocity;
		}

		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

		float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmooting, (controller.collisions.below) ? acclerationTimeGrounded : acclerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime);
	}
}
