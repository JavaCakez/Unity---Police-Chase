using UnityEngine;
using System.Collections;

public class CarExplosion : MonoBehaviour {

	public ParticleSystem explosion;
	public HoverMotor hoverMotor;
	public AudioSource explosionSound;
	public AudioSource crashSound;

	private bool exploded = false;
	
	void OnCollisionEnter(Collision coll) {
		if (!exploded) {
			exploded = true;
			Vector3 velocity = coll.relativeVelocity;
			Rigidbody carRigidBody = gameObject.GetComponent<Rigidbody> ();
			carRigidBody.constraints &= ~RigidbodyConstraints.FreezeRotationX;
			carRigidBody.constraints &= ~RigidbodyConstraints.FreezeRotationZ;
			hoverMotor.setHovering (false);
			explosion.Play ();
			carRigidBody.AddTorque (-velocity.x * 100.0f, -velocity.y * 100.0f, -velocity.z * 100.0f);
			explosionSound.Play();
			crashSound.Play();
		}
	}
}
