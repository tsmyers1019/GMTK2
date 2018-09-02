using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour {

	public LayeredText countdownText;
	public string readyText, goText, winText, loseText, nextText, tryAgainText;
	public int countdownFrom;
	public float initialWaitTime, countdownIntervalTime;

	[HideInInspector] public bool go;
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

	private void Start() {

		// lock controls
		foreach(Car car in cars) {
			car.locked = true;
		}

		// countdown to unlock controls
		StartCoroutine(readySetGo());
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

	private IEnumerator readySetGo() {

		// READY...
		countdownText.Text = readyText;
		yield return new WaitForSeconds(initialWaitTime);

		// 3... 2... 1...
		for(int count = 3; count > 0; count--) {
			countdownText.Text = ""+count;
			yield return new WaitForSeconds(countdownIntervalTime);
		}

		// GO!
		countdownText.Text = goText;
		RaceStart();

		// clear
		yield return new WaitForSeconds(countdownIntervalTime);
		countdownText.Text = "";
	}

	private IEnumerator endText() {

		bool win = GetComponentInChildren<lapTracking>().orderedCars[0].tag == "Player";

		while(true) {
			countdownText.Text = win ? winText : loseText;
			yield return new WaitForSeconds(countdownIntervalTime);
			countdownText.Text = win ? nextText : tryAgainText;
			yield return new WaitForSeconds(countdownIntervalTime);
		}
	}

	public void RaceStart() {

		go = true;
		
		foreach(Car car in cars) {
			car.locked = false;
		}
	}

	public void RaceEnd() {

		go = false;

		StartCoroutine(endText());

		foreach(Car car in cars) {
			if(car is PlayerCar) {
				((PlayerCar)car).AutoPilot();
			}
		}
	}
}
