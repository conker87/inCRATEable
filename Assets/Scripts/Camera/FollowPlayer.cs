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

	void FindPlayerIfNull() {

		if (player == null) {

			player = GameObject.FindGameObjectWithTag ("Player");

			if (player == null) {

				Debug.LogError ("Player cannot be found a second time, there is something seriously wrong!");

			}

		}

	}

}
