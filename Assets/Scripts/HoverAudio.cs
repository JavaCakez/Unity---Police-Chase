using UnityEngine;
using System.Collections;

public class HoverAudio : MonoBehaviour {

	public AudioSource jetSound;
	private float jetPitch;
	private const float LowPitch = 0.1f;
	private const float HighPitch = 2.0f;
	private const float SpeedToRevs = 0.025f;
	Rigidbody carRigidbody;
	
	void Awake () {
		carRigidbody = GetComponent<Rigidbody> ();
	}

	private void FixedUpdate () {
		// converts from world space to local space
		float forwardSpeed = transform.InverseTransformDirection (carRigidbody.velocity).z;
		float engineRevs = Mathf.Abs (forwardSpeed) * SpeedToRevs;
		jetSound.pitch = Mathf.Clamp (engineRevs, LowPitch, HighPitch);
	}
}
