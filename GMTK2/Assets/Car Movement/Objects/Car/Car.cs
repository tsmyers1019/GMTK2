using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

	// Inspector attributes
	public float maxEngineForce, maxReverseForce, engineForceDecay, acceleration, brakingMultiplier, turning, groundedCheckMargin;

	// Properties
	protected Rigidbody rb;
	private bool isGrounded {
		get {
			return Physics.Raycast(transform.position, transform.rotation * Vector3.down, transform.localScale.y / 2 + groundedCheckMargin);
		}
	}
	[HideInInspector] public float engineForce, turningForce;

	private void Start() {
		rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate() {

		if(isGrounded) {

			engineForce = Mathf.Clamp(engineForce, -maxReverseForce, maxEngineForce);
			rb.AddRelativeForce(Vector3.forward * engineForce);
			
			float a = 0.5f; // this is how much turning you can do standing still
			turningForce *= a + ((1 - a) * (Mathf.Abs(engineForce) / maxEngineForce));
			rb.AddRelativeTorque(Vector3.up * turningForce);
		}
		
		engineForce -= engineForce * engineForceDecay;
		turningForce = 0;
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
			engineForce -= acceleration * brakingMultiplier * Time.deltaTime;
		}
	}

	protected void TurnLeft() {
		if(isGrounded) {
			turningForce = -turning;
		}
	}

	protected void TurnRight() {
		if(isGrounded) {
			turningForce = turning;
		}
	}
}
