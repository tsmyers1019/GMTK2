using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class carNoises : MonoBehaviour {

	AudioSource audioData;

	public AudioClip[] builds;
	public AudioClip[] loops;

	public float gear1;
	public float gear2;
	public float gear3;

	public float readEngineForce;
	public float myEngineForce;
	public float myMaxEngineForce;

	private int currentGear;

	void Start () {
		audioData = GetComponent <AudioSource>();
	}
	
	void Update () {

		myEngineForce = gameObject.GetComponent<Car>().engineForce;
		myMaxEngineForce = gameObject.GetComponent<Car>().maxEngineForce;

		readEngineForce = myEngineForce / myMaxEngineForce;

		if (readEngineForce > gear3) {
			doGear(3);
		}
		else if (readEngineForce > gear2) {
			doGear(2);
		}
		else if (readEngineForce > gear1) {
			doGear(1);
		}
		else if (readEngineForce < gear1) {
			doGear(0);
		}
	}

	private void doGear(int newGear) {

		if (currentGear != newGear) {

			currentGear = newGear;

			audioData.clip = builds[newGear];
			audioData.loop = false;
			audioData.Play(0);
		}

		if (currentGear == newGear && audioData.isPlaying == false) {

			audioData.clip = loops[newGear];
			audioData.loop = !(newGear == 1); // don't loop on gear 1
			audioData.Play(0);
		}
	}
}
