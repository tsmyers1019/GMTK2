using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

	// Inspector attributes
	public float acceleration, braking, turning, turningVelocityMultiplier;

	// Properties
	private Rigidbody rb;
	private bool isGrounded {
		get {
			return Physics.Raycast(transform.position, transform.rotation * Vector3.down, transform.localScale.y / 2 + 0.01f);
		}
	}

	private void Start() {
		rb = GetComponent<Rigidbody>();
	}

	protected void Accelerate() {
		if(isGrounded) {
			rb.AddRelativeForce(Vector3.forward * acceleration);
		}
	}

	protected void Brake() {
		if(isGrounded) {
			rb.AddRelativeForce(Vector3.back * braking);
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
