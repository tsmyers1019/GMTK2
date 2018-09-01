using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour {

	[HideInInspector] public Waypoint[] waypoints {
		get {
			return GetComponentsInChildren<Waypoint>();
		}
	}
	[HideInInspector] public Car[] cars {
		get {
			return GetComponentsInChildren<Car>();
		}
	}

	private void OnDrawGizmos() {

		for(int i = 0; i < waypoints.Length; i++) {
			int next = i + 1;
			if(next == waypoints.Length) {
				next = 0;
			}
			Gizmos.DrawLine(waypoints[i].transform.position, waypoints[next].transform.position);
		}
	}
}
