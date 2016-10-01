using UnityEngine;
using System.Collections;

public class Wrap2D : MonoBehaviour {

	public float wrapPosition = 5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (transform.position.x < -wrapPosition) {

			transform.position = new Vector2 (wrapPosition, transform.position.y);

		}

		if (transform.position.x > wrapPosition) {

			transform.position = new Vector2 (-wrapPosition, transform.position.y);

		}

	}
}
