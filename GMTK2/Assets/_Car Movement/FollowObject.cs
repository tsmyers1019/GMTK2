using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

	public Transform objectToFollow;

	private Vector3 offset;

	private void Start() {
		offset = transform.position;
	}

	private void Update() {
		transform.position = objectToFollow.position + offset;
	}
}
