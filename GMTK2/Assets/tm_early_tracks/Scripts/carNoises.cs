using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class carNoises : MonoBehaviour {


	AudioSource audioData;

	public AudioClip hi2hiB;
	public AudioClip hi2hiL;
	public AudioClip mid2hiB;
	public AudioClip mid2hiL;
	public AudioClip lo2hiB;
	public AudioClip lo2hiL;

	public AudioClip currentPlay;


	public float readEngineForce;
	public float myEngineForce;
	public float myMaxEngineForce;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		myEngineForce = gameObject.GetComponent <Car>().engineForce;
		myMaxEngineForce = gameObject.GetComponent <Car>().maxEngineForce;

		readEngineForce = myEngineForce/myMaxEngineForce;// 1 == 100 %


		if (readEngineForce > 0.33f ){// 33%

		
		audioData = GetComponent <AudioSource>();
		audioData = hi2hiB;
		audioData.Play(0);




		}
	}
}
