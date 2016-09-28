using UnityEngine;
using System.Collections;

public class Wrap2D : MonoBehaviour {

	public float cutoffPosition = 5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (transform.position.x < -cutoffPosition) {

			transform.position = new Vector2 (cutoffPosition, transform.position.y);

		}

		if (transform.position.x > cutoffPosition) {

			transform.position = new Vector2 (-cutoffPosition, transform.position.y);

		}

	}
}
