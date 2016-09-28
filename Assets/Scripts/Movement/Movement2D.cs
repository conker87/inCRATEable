using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement2D : MonoBehaviour {

	public float moveSpeed = 4f, accelerometerDeadzone = 0f;

	float h;

	Rigidbody2D rigidbody2d;


	void Start () {

		rigidbody2d = GetComponent<Rigidbody2D> ();

	}

	void Update () {
	
		h = (Input.acceleration.x > -accelerometerDeadzone && Input.acceleration.x < accelerometerDeadzone) ? 0f : Input.acceleration.x;

		if (Input.GetAxis("Horizontal") != 0f) {

			h = Input.GetAxis("Horizontal");

		}

		rigidbody2d.velocity = new Vector2(h * moveSpeed, rigidbody2d.velocity.y);

	}
}
