using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapCounter : MonoBehaviour {

	public Car car;

	private int lap;

	public void UpdateLapCounter(int newLap) {
		newLap = Mathf.Abs(newLap);
		if(newLap > lap) {
			lap = newLap;
			GetComponent<Text>().text = ""+lap;
		}
	}
}
