using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float distance = 3.0f;
	public float height = 3.0f;
	public float damping = 5.0f;
	public bool smoothRotation = true;
	public bool followBehind = true;
	public float rotationDamping = 10.0f;

	void Update () {
		Vector3 wantedPosition;
		if (followBehind)
			wantedPosition = target.TransformPoint (0, height, -distance);
		else
			wantedPosition = target.TransformPoint (0, height, distance);
	
		transform.position = Vector3.Lerp (transform.position, wantedPosition, Mathf.SmoothStep (0.0f, 1.0f, Time.deltaTime * damping));
		if (transform.position.y < 5.0f) transform.position = new Vector3(transform.position.x, 5.0f, transform.position.z);
		transform.position = (transform.position - target.transform.position).normalized * 20.0f + target.transform.position;
	
		if (smoothRotation) {
			Quaternion wantedRotation = Quaternion.LookRotation (target.position - transform.position, target.up);
			transform.rotation = Quaternion.Slerp (transform.rotation, wantedRotation, Mathf.SmoothStep (0.0f, 1.0f, Time.deltaTime * rotationDamping));
		} else
			transform.LookAt (target, Vector3.up);
	
	}
	
}
