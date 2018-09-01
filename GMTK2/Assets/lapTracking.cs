using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lapTracking : MonoBehaviour {

	public float lapNum = 0;

	public float lapLim = 3;
	public bool raceEnd;
	public float angle;

	public Vector3 direction;

	//private Transform lineOrientation;

	//public GameObject line;


	// Use this for initialization
	void Start () {
		//lapNum = 0;
		raceEnd = false;

		//lineOrientation = line.GetComponant<transform>();
		

		
	}
	
	// Update is called once per frame
	void Update () {

		
		
	}
	
	void OnTriggerEnter (Collider other) {

		angle = Quaternion.Angle (transform.rotation, other.transform.rotation);

		if (other.tag == "Player"|| other.tag == "AIcar"){

	direction = other.GetComponent<Rigidbody>().velocity.normalized;

	float worldDegrees = Vector3.Angle(Vector3.forward, direction); // angle relative to world space
	float localDegrees = Vector3.Angle(other.transform.forward, direction); // angle relative to last heading of myobject

			if (localDegrees - worldDegrees <= 0){// might need to flip th

				if (angle <= 90){
					if (raceEnd == false){

							lapNum += 1;
						}
					}

				if (angle >= 90){
					if (raceEnd == false){

							lapNum -= 1;
						}
					}

				}
			
			if (localDegrees - worldDegrees >= 0){// might need to flip th
					if (raceEnd == false){

									lapNum -= 1;
								}
			}


			if (lapNum == lapLim){

					raceEnd = true;
				}


		}
	
	}

	//void OnTriggerExit(Collider other){

	//float worldDegrees = Vector3.Angle(Vector3.forward, direction); // angle relative to world space
	//float localDegrees = Vector3.Angle(other.transform.forward, direction); // angle relative to last heading of myobject
	//if (localDegrees - worldDegrees <= 0){// might need to flip th
	//				if (raceEnd == false){
//
//									lapNum -= 1;
//								}
//			}

//	}
}
