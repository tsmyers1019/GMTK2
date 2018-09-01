using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : Car {

	// Public fields
	public KeyCode accelerate, brake, turnLeft, turnRight;

	private void Update() {

		if(Input.GetKey(accelerate)) {
			Accelerate();
		}
		if(Input.GetKey(brake)) {
			Brake();
		}

		float angleOffset = Vector3.Angle(transform.rotation * Vector3.forward, rb.velocity.normalized);
		bool reversing = angleOffset > 90;

		if(Input.GetKey(turnLeft)) {
			if(reversing) {
				TurnRight();
			}
			else {
				TurnLeft();
			}
		}
		if(Input.GetKey(turnRight)) {
			if(reversing) {
				TurnLeft();
			}
			else {
				TurnRight();
			}
		}
	}
}
