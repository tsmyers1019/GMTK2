using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

	// Inspector attributes
	public float maxEngineForce, maxReverseForce, engineForceDecay, acceleration, braking, turning, turningVelocityMultiplier, groundedCheckMargin;

	// Properties
	private Rigidbody rb;
	private bool isGrounded {
		get {
			return Physics.Raycast(transform.position, transform.rotation * Vector3.down, transform.localScale.y / 2 + groundedCheckMargin);
		}
	}
	private float engineForce;

	private void Start() {
		rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate() {
		Debug.Log(engineForce / maxEngineForce);
		engineForce -= engineForce * engineForceDecay;
		engineForce = Mathf.Clamp(engineForce, -maxReverseForce, maxEngineForce);
		rb.AddRelativeForce(Vector3.forward * engineForce);
	}

	private void OnCollisionEnter(Collision c) {
		engineForce /= 2;
		rb.AddForce(-c.relativeVelocity);
	}

	protected void Accelerate() {
		if(isGrounded) {
			engineForce += acceleration * Time.deltaTime;
		}
	}

	protected void Brake() {
		if(isGrounded) {
			engineForce -= braking * Time.deltaTime;
		}
	}

	protected void TurnLeft() {
		if(isGrounded) {
			rb.AddRelativeTorque(Vector3.down * turning + Vector3.down * rb.velocity.magnitude * turningVelocityMultiplier);
		}
	}

	protected void TurnRight() {
		if(isGrounded) {
			rb.AddRelativeTorque(Vector3.up * turning + Vector3.up * rb.velocity.magnitude * turningVelocityMultiplier);
		}
	}
}
