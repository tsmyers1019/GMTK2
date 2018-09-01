using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lapTracking : MonoBehaviour {

	public int lapLim;
	public Track track;

	private Dictionary<Car, int> laps = new Dictionary<Car, int>();

	// Register all cars as keys for the laps dictionary
	private void Start() {
		foreach(Car car in track.cars) {
			laps.Add(car, 0);
		}
	}

	// Return the angle between the RigidBody's velocity vector and this line
	private float getAngle(Rigidbody rb) {
		Vector3 direction = rb.velocity.normalized;
		return Vector3.SignedAngle(transform.rotation * Vector3.forward, direction, Vector3.up);
	}

	private void OnTriggerEnter(Collider other) {

		// Check if the collider is a car
		Car car = other.GetComponent<Car>();
		if(car) {

			// Add a lap for this car if it's going forward
			if(getAngle(car.GetComponent<Rigidbody>()) > 0) {

				laps[car]++;

				Debug.Log(string.Format(
					"{0}:\t{1} / {2}",
					car.name,
					laps[car],
					lapLim
				));

				if(Mathf.Abs(laps[car]) == lapLim) {
					//TODO: end race
				}
			}
		}
	}

	private void OnTriggerExit(Collider other) {

		// Check if the collider is a car
		Car car = other.GetComponent<Car>();
		if(car) {

			// Subtract a lap for this car if it's going backward
			if(getAngle(car.GetComponent<Rigidbody>()) < 0) {

				laps[car]--;

				Debug.Log(string.Format(
					"{0}:\t{1} / {2}",
					car.name,
					laps[car],
					lapLim
				));

				if(Mathf.Abs(laps[car]) == lapLim) {
					//TODO: end race
				}
			}
		}
	}
}
