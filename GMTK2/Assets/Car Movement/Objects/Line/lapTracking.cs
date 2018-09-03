using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class lapTracking : MonoBehaviour {

	public int lapLim;
	public Track track;
	public GameObject lapCounterContainer;
	public LayerMask trackMask;

	private Dictionary<Car, int> laps = new Dictionary<Car, int>();
	private Dictionary<Car, int> waypoints = new Dictionary<Car, int>();
	private Dictionary<Car, LapCounter> lapCounters = new Dictionary<Car, LapCounter>();
	[HideInInspector] public Car[] orderedCars;

	private void Start() {

		// Register all cars as keys for the laps dictionary
		foreach(Car car in track.cars) {
			laps.Add(car, 0);
		}

		// Register all waypoints
		foreach(Car car in track.cars) {
			waypoints.Add(car, 0);
		}
		StartCoroutine(checkLapProgress());

		// Register all lapCounters
		foreach(LapCounter counter in lapCounterContainer.GetComponentsInChildren<LapCounter>()) {
			foreach(Car car in track.cars) {
				if(counter.car == car) {
					lapCounters.Add(car, counter);
					break;
				}
			}
		}
	}

	private IEnumerator checkLapProgress() {
		while(true) {

			// wait if race is not going
			if(!track.go) {
				yield return new WaitForFixedUpdate();
			}

			else {

				foreach(Car car in track.cars) {
					if(Physics.CheckSphere(car.transform.position, 10, trackMask)) {
						
						// sort waypoints by distance
						Waypoint[] sortedWaypoints = track.waypoints.OrderBy(x => Vector3.Distance(x.transform.position, car.transform
						.position)).ToArray();

						// get nearest waypoint's index in track.waypoints
						int nearestWaypointIndex = Array.IndexOf(track.waypoints, sortedWaypoints[0]);

						// set waypoints[car] to that index
						waypoints[car] = nearestWaypointIndex;

						Debug.Log(car.name + " " + waypoints[car]);
					}
					yield return new WaitForFixedUpdate();
				}

				orderedCars = track.cars.OrderByDescending(car => laps[car] * 10000 + waypoints[car]).ToArray();
				for(int position = 0; position < orderedCars.Length; position++) {
					lapCounters[orderedCars[position]].UpdateLapCounter(position);
				}	
			}
		}
	}

	// Return the angle between the RigidBody's velocity vector and this line
	private float getAngle(Rigidbody rb) {
		Vector3 direction = rb.velocity.normalized;
		return Vector3.SignedAngle(transform.rotation * Vector3.forward, direction, Vector3.up);
	}

	private void OnTriggerEnter(Collider other) {

		if(track.go) {

			// Check if the collider is a car
			Car car = other.GetComponent<Car>();
			if(car) {

				// Add a lap for this car if it's going forward
				if(getAngle(car.GetComponent<Rigidbody>()) > 0) {

					laps[car]++;
					lapCounters[car].UpdateLapCounter(laps[car]);

					if(Mathf.Abs(laps[car]) > lapLim) {
						track.RaceEnd();
					}

					Debug.Log(car.name + laps[car]);
				}
			}
		}
	}

	private void OnTriggerExit(Collider other) {

		if(track.go) {

			// Check if the collider is a car
			Car car = other.GetComponent<Car>();
			if(car) {

				// Subtract a lap for this car if it's going backward
				if(getAngle(car.GetComponent<Rigidbody>()) < 0) {

					laps[car]--;
					//lapCounters[car].UpdateLapCounter(laps[car]);

					if(Mathf.Abs(laps[car]) > lapLim) {
						track.RaceEnd();
					}
				}
			}
		}
	}
}
