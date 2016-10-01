using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement2D : MonoBehaviour {

	public float moveSpeed = 4f, accelerationDeadzone = 0f, accelerationSpeedScale = 1.5f;

	float horitontalMovement;

	Rigidbody2D rigidbody2d;


	void Start () {

		rigidbody2d = GetComponent<Rigidbody2D> ();

	}

	void Update () {

		if (GameManager.instance.paused || GameManager.instance.gameOver) {



		} else {

			DoMovement ();

		}

	}

	void DoMovement() {

		horitontalMovement = (Input.acceleration.x > -accelerationDeadzone && Input.acceleration.x < accelerationDeadzone) ? 0f : Input.acceleration.x * accelerationSpeedScale;

		if (Input.GetAxis("Horizontal") != 0f) {

			horitontalMovement = Input.GetAxis("Horizontal");

		}

		rigidbody2d.velocity = new Vector2(horitontalMovement * moveSpeed, rigidbody2d.velocity.y);

	}
}
