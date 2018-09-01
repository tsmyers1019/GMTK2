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

	private bool initial;

	private bool initial2;
	private bool initial3;
	private bool initial4;


	public float readEngineForce;
	public float myEngineForce;
	public float myMaxEngineForce;
	// Use this for initialization
	void Start () {
		audioData = GetComponent <AudioSource>();
		initial = true;
		initial2 = true;
		initial3 = true;
		initial4 = true;
		
	}
	
	// Update is called once per frame
	void Update () {
		myEngineForce = gameObject.GetComponent <Car>().engineForce;
		myMaxEngineForce = gameObject.GetComponent <Car>().maxEngineForce;

		readEngineForce = myEngineForce/myMaxEngineForce;// 1 == 100 %


		if (readEngineForce > 0.01f ){// 0%

		//currentPlay = audioData.clip;
		//currentPlay= hi2hiB;
		//audioData.Play(0);
		}

		if (readEngineForce < 0.01f ){
			if (initial4 == false){
			initial4 = true;
			audioData.Stop();
			}
		}


		if (readEngineForce > 0.33f ){// 33% THESE NEED TO BE MUCH HIGHER`

				if (initial == true){
					initial = false;
					currentPlay= hi2hiB;// tell it what to be
					audioData.clip = currentPlay;// tell it where to be
					audioData.loop = false;
					audioData.Play(0);

				}

				if (initial == false){
					if(audioData.isPlaying == false){
						currentPlay= hi2hiL;
						audioData.clip = currentPlay;
						audioData.loop = true;
						audioData.Play(0);
					}
				}




		}
		if (readEngineForce < 0.33f ){
			if (initial == false){
			initial = true;
			audioData.Stop();
			}
		}


		if (readEngineForce > 0.66f ){// 66%// because there is only one audio source this will cover up the above'



				if (initial2 == true){
					initial2 = false;
					currentPlay= mid2hiB;// tell it what to be
					audioData.clip = currentPlay;// tell it where to be
					audioData.loop = false;
					audioData.Play(0);

				}

				if (initial2 == false){
					if(audioData.isPlaying == false){// the build clip has finished
						currentPlay= mid2hiL;
						audioData.clip = currentPlay;
						audioData.loop = true;
						audioData.Play(0);
					}
				}




		}
		if (readEngineForce < 0.66f ){
			if (initial2 == false){
					initial2 = true;
					audioData.Stop();
			}
				}


		if (readEngineForce > 0.99f ){// 99%// because there is only one audio source this will cover up the above'



				if (initial3 == true){
					initial3 = false;
					currentPlay= lo2hiB;// tell it what to be
					audioData.clip = currentPlay;// tell it where to be
					audioData.loop = false;
					audioData.Play(0);

				}

				if (initial3 == false){
					if(audioData.isPlaying == false){// the build clip has finished
						currentPlay= lo2hiL;
						audioData.clip = currentPlay;
						audioData.loop = true;
						audioData.Play(0);
					}
				}




		}
		if (readEngineForce < 0.99f ){
			if (initial3 == false){
			initial3 = true;
			audioData.Stop();
			}
		}



	}
}
