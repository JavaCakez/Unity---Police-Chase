using UnityEngine;
using System.Collections;

public class PoliceCarAI : MonoBehaviour {

	public float speed = 40.0f;
	public float maxSpeed = 50.0f;
	public float turnSpeed = 1.0f;
	public GameObject userCar;
	public const float CollisionDetectionDistance = 30.0f;
	
	private float powerInput;
	private float turnInput;
	private Vector3 turnVector;
	private Rigidbody carRigidbody;
	private bool facingObstacle = false;
	private float obstacleTurnForce; 
	private float proportionalDistanceToCollision = 0.0f;
	private GameObject collisionGameObject;

	int x = 0;

	void Awake () {
		carRigidbody = GetComponent<Rigidbody>();
	}
	
	void Update () {
		// Don't want physics in update since we don't want it reliant on framerate
		powerInput = 1.0f;
		turnInput = transform.InverseTransformPoint (userCar.transform.position).x;
		turnInput = Mathf.Clamp (turnInput, -1.0f, 1.0f);
	}
	
	void FixedUpdate() {
		//Cast ray directly foraward
		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.forward, out hit, CollisionDetectionDistance) && hit.collider.gameObject.tag != "Player") {
			collisionGameObject = hit.collider.gameObject;
			if (facingObstacle == false) {
				// randomly choose left or right
				if (Random.value < 0.5f) 
					obstacleTurnForce = -1.0f;
				else
					obstacleTurnForce = 1.0f;
				facingObstacle = true;
			}

			proportionalDistanceToCollision = (CollisionDetectionDistance - hit.distance) / CollisionDetectionDistance;
			carRigidbody.AddRelativeTorque (0f, obstacleTurnForce * proportionalDistanceToCollision * turnSpeed, 0f);
		} else {
			facingObstacle = false;



			float distanceBetweenCars = Vector3.Distance(transform.position, userCar.transform.position);
			carRigidbody.AddRelativeTorque (0f, turnInput * turnSpeed, 0f);
			carRigidbody.AddRelativeForce (0f, 0f, powerInput * speed);
			if (carRigidbody.velocity.magnitude > maxSpeed) {
				carRigidbody.velocity = carRigidbody.velocity.normalized * maxSpeed;
			}
		}


	}

	/*
if (Physics.Raycast (transform.position, transform.forward, out hit, CollisionDetectionDistance) && hit.collider.gameObject.tag != "Player") {
			collisionGameObject = hit.collider.gameObject;
			passedObstacle = false;
			if (facingObstacle == false) {
				// randomly choose left or right
				if (Random.value < 0.5f) 
					obstacleTurnForce = -1.0f;
				else
					obstacleTurnForce = 1.0f;
				facingObstacle = true;
			}

			proportionalDistanceToCollision = (CollisionDetectionDistance - hit.distance) / CollisionDetectionDistance;
			carRigidbody.AddRelativeTorque (0f, obstacleTurnForce * proportionalDistanceToCollision * turnSpeed, 0f);
		} else {
			if (facingObstacle == true) {
				//Once no longer facing an obstacle equalise out the torque to go straight
				carRigidbody.AddRelativeTorque (0f, -obstacleTurnForce * proportionalDistanceToCollision * turnSpeed, 0f);
				facingObstacle = false;
				proportionalDistanceToCollision = 0.0f;
			}

			// Dont try to face car until passed obstacle
			if (passedObstacle == false) {
				Vector3 oppositeObstacleTurnDirection = transform.right * -obstacleTurnForce;
				if (Physics.Raycast (transform.position, oppositeObstacleTurnDirection, out hit, CollisionDetectionDistance) && hit.collider.gameObject == collisionGameObject) {
					passedObstacle = true;
				}
			} else {
				float distanceBetweenCars = Vector3.Distance(transform.position, userCar.transform.position);
				carRigidbody.AddRelativeTorque (0f, turnInput * turnSpeed, 0f);
			}
		}
	 */
}
