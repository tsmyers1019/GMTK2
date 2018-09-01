using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboCar : Car {

	public Track track;
	public float turningThreshold, acceleratingThreshold, waypointCountThreshold;

	private int waypointCount;

	private void Update() {

		Vector3 nextWaypoint = track.waypoints[waypointCount].transform.position;
		Vector3 towardsNextWaypoint = (nextWaypoint - transform.position).normalized;
		float angleOffset = Vector3.SignedAngle(transform.rotation * Vector3.forward, towardsNextWaypoint, Vector3.up);

		if(angleOffset < -turningThreshold) {
			TurnLeft();
		}
		else if(angleOffset > turningThreshold) {
			TurnRight();
		}

		if(Mathf.Abs(angleOffset) < acceleratingThreshold) {
			Accelerate();
		}
		else {
			Brake();
		}

		if(Vector3.Distance(transform.position, nextWaypoint) < waypointCountThreshold) {
			waypointCount++;
			if(waypointCount == track.waypoints.Length) {
				waypointCount = 0;
			}
		}
	}
}
