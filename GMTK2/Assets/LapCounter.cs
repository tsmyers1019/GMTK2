using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapCounter : MonoBehaviour {

	/*
	THIS IS BEING USED FOR PLACE INSTEAD OF LAP
	OOOOOPS
	 */

	public Car car;

	private int lap;

	/*
	public void UpdateLapCounter(int newLap) {
		newLap = Mathf.Abs(newLap);
		if(newLap > lap) {
			lap = newLap;
			GetComponent<Text>().text = ""+lap;
		}
	}
	*/

	public void UpdateLapCounter(int place) {
		place = place + 1; // 0 = 1st place
		string suffix = "";
		switch(place) {
			case 1:
				suffix = "st";
				break;
			case 2:
				suffix = "nd";
				break;
			case 3:
				suffix = "rd";
				break;
			default:
				suffix = "th";
				break;
		}
		GetComponent<Text>().text = place + suffix;
	}
}
