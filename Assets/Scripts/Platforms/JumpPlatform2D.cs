using UnityEngine;
using System.Collections;

public class JumpPlatform2D : MonoBehaviour {

	public float jumpHeight = 3000f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other) {

		if (other.tag == "Player") {

			Debug.Log ("HitPlayer ");

		}

	}
}
