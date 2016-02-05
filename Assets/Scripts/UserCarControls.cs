using UnityEngine;
using System.Collections;

public class UserCarControls : MonoBehaviour {
	
	public float speed = 30.0f;
	public float maxSpeed = 40.0f;
	public float turnSpeed = 2f;
	
	private float powerInput;
	private float turnInput;
	private Rigidbody carRigidbody;
	
	void Awake () {
		carRigidbody = GetComponent<Rigidbody>();
	}
	
	void Update () {
		// Don't want physics in update since we don't want it reliant on framerate
		powerInput = Input.GetAxis ("Vertical");
		turnInput = Input.GetAxis ("Horizontal");
	}
	
	void FixedUpdate() {
		// Use powerInput to force car along Z axis
		// Use turn input to rotate around Y axis to move left / right
		carRigidbody.AddRelativeForce (0f, 0f, powerInput * speed);
		if (carRigidbody.velocity.magnitude > maxSpeed) {
			carRigidbody.velocity = carRigidbody.velocity.normalized * maxSpeed;
		}
		carRigidbody.AddRelativeTorque (0f, turnInput * turnSpeed, 0f);
	}
}
