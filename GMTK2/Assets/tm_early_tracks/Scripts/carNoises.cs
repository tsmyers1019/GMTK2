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

	// Use this for initialization
	void Start () {
		audioData = GetComponent <AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

		myEngineForce = gameObject.GetComponent <Car>().engineForce;
		myMaxEngineForce = gameObject.GetComponent <Car>().maxEngineForce;

		readEngineForce = myEngineForce/myMaxEngineForce;// 1 == 100 %

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
			audioData.Stop();
		}
	}

	private void doGear(int newGear) {

		if (currentGear != newGear) {

			Debug.Log("gear" + newGear);
			//Debug.Break();

			currentGear = newGear;

			audioData.clip = builds[newGear - 1];// tell it where to be
			audioData.loop = false;
			audioData.Play(0);
		}

		if (currentGear == newGear && audioData.isPlaying == false) {

			audioData.clip = loops[newGear - 1];
			audioData.loop = true;
			audioData.Play(0);
		}
	}
}
