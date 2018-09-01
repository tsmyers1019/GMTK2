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
		if(Input.GetKey(turnLeft)) {
			TurnLeft();
		}
		if(Input.GetKey(turnRight)) {
			TurnRight();
		}
	}
}
