﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

	// Inspector attributes
	public float maxEngineForce, maxReverseForce, engineForceDecay, acceleration, brakingMultiplier, turning, turningEngineCutFactor, groundedCheckMargin,airDrag,airAdrag;
	private float drag_prev, Adrag_prev;

	// Properties
	protected Rigidbody rb;
	private bool isGrounded {
		get {// this is how we are checking for grounded
			return Physics.Raycast(transform.position, transform.rotation * Vector3.down, transform.localScale.y / 2 + groundedCheckMargin);
		}
	}
	[HideInInspector] public float engineForce, turningForce;
	[HideInInspector] public bool locked;

	private void Start() {
		rb = GetComponent<Rigidbody>();
		drag_prev = rb.drag;
		Adrag_prev = rb.angularDrag;

		airDrag = 0.2f;
		airAdrag = 0.2f;
	}

	private void FixedUpdate() {

		if(!locked && isGrounded) {
			
			float a = 0.5f; // this is how much turning you can do standing still
			turningForce *= a + ((1 - a) * (Mathf.Abs(engineForce) / maxEngineForce));
			rb.AddRelativeTorque(Vector3.up * turningForce);

			engineForce -= engineForce * (turningEngineCutFactor * (Mathf.Abs(turningForce) / turning));

			engineForce = Mathf.Clamp(engineForce, -maxReverseForce, maxEngineForce);
			rb.AddRelativeForce(Vector3.forward * engineForce);

			rb.drag = drag_prev;
			rb.angularDrag = Adrag_prev;
		}
		
		engineForce -= engineForce * engineForceDecay;
		turningForce = 0;

		// tom code
		if (isGrounded == false) {
			rb.drag = airDrag;
			rb.angularDrag = airAdrag;
		}
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
