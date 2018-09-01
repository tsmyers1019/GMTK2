using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour {

	[HideInInspector] public Waypoint[] waypoints;
	[HideInInspector] public Car[] cars;

	private void OnDrawGizmos() {

		waypoints = GetComponentsInChildren<Waypoint>();
		cars = GetComponentsInChildren<Car>();

		for(int i = 0; i < waypoints.Length; i++) {
			int next = i + 1;
			if(next == waypoints.Length) {
				next = 0;
			}
			Gizmos.DrawLine(waypoints[i].transform.position, waypoints[next].transform.position);
		}
	}
}
