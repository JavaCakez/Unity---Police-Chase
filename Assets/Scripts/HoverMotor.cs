using UnityEngine;
using System.Collections;

public class HoverMotor : MonoBehaviour {
	
	public float hoverForce = 65f;
	public float hoverHeight = 3.5f;
	public bool hovering = true;

	private float powerInput;
	private float turnInput;
	private Rigidbody carRigidbody;
	
	void Awake () {
		carRigidbody = GetComponent<Rigidbody>();
	}

	void FixedUpdate() {
		if (hovering) {
			Ray ray = new Ray (transform.position, -transform.up);
			RaycastHit hit;

			// Cast a ray down to calculate distance to ground. Use this to apply force depending on how far it is away from ground.
			if (Physics.Raycast (ray, out hit, hoverHeight)) {
				float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
				Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;
				// ForceMode.Acceleration ignores the mass of the car which gives a smoother hover
				carRigidbody.AddForce (appliedHoverForce, ForceMode.Acceleration);
			}
		} else {
			carRigidbody.mass = carRigidbody.mass + 10.0f;
		}
	}

	public void setHovering(bool isHovering) {
		hovering = isHovering;
	}
}
