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

	public void AutoPilot() {

		return;

		RoboCar roboCar = gameObject.AddComponent<RoboCar>();

		// hard coded values lol
		roboCar.maxEngineForce = 200000;
		roboCar.maxReverseForce = 50000;
		roboCar.engineForceDecay = 0.01f;
		roboCar.acceleration = 100000;
		roboCar.brakingMultiplier = 1.5f;
		roboCar.turning = 150000;
		roboCar.groundedCheckMargin = 0.25f;
		roboCar.track = transform.parent.parent.GetComponent<Track>();
		roboCar.turningThreshold = 5;
		roboCar.acceleratingThreshold = 15;
		roboCar.waypointCountThreshold = 27;

		Destroy(this);
	}
}
