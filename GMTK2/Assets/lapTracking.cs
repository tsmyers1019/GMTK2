using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lapTracking : MonoBehaviour {

	public int lapLim;
	public Track track;

	private Dictionary<Car, int> laps = new Dictionary<Car, int>();

	// Register all cars as keys for the laps dictionary
	// This needs to be a coroutine because - for some reason - the first frame of the game does not count the cars correctly. (Something to do with Track.OnDrawGizmos but it's really not worth figuring out.)
	private void Start() {
		StartCoroutine(registerCars());
	}
	private IEnumerator registerCars() {
		Debug.Log(track.cars.Length);
		yield return new WaitForSeconds(1);
		foreach(Car car in track.cars) {
			laps.Add(car, 0);
		}
		Debug.Log(track.cars.Length);
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
