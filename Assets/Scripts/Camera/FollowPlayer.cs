using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	public GameObject player;

	void Start() {

		player = GameObject.FindGameObjectWithTag ("Player");

	}

	void Update () {
		transform.position = new Vector3 (0.0f, transform.position.y, -10f);
	}



}
