using UnityEngine;
using System.Collections;

public class Jump2D : MonoBehaviour {

	[SerializeField]
	bool grounded;
	[SerializeField]
	Collider2D groundHit;
	JumpPlatform2D jp;

	public float jumpHeight = 500f, originalJumpHeight;
	public Transform[] groundCheck;

	public float groundRadius = .2f;
	public LayerMask grounds;

	float velocityY;

	Vector3 tempVelocity;

	Rigidbody2D rigidbody2d;

	bool justUnpaused = false;

	void Start () {

		rigidbody2d = GetComponent<Rigidbody2D> ();

		originalJumpHeight = jumpHeight;
	}

	void FixedUpdate () {
	
		if (GameManager.instance.paused || GameManager.instance.gameOver) {

			rigidbody2d.isKinematic = true;

			justUnpaused = true;

		} else {

			rigidbody2d.isKinematic = false;

			if (justUnpaused) {
				
				rigidbody2d.velocity = tempVelocity;
				justUnpaused = false;

			}

			DoMovement ();

		}

	}

	void DoMovement() {

		if (groundCheck != null) {

			foreach (Transform t in groundCheck) {

				grounded = groundHit = Physics2D.OverlapCircle (t.position, groundRadius, grounds);

				if (groundHit != null) {

					if (velocityY <= 0) {

						if ((jp = groundHit.transform.parent.GetComponent<JumpPlatform2D> ()) != null) {

							jumpHeight = jp.jumpHeight;

						}

						if (grounded) {

							Jump (jumpHeight);

							jumpHeight = originalJumpHeight;

							break;

						}

					}

				}

			}

		}

		velocityY = rigidbody2d.velocity.y;
		tempVelocity = rigidbody2d.velocity;

	}

	void Jump(float jumpHeight) {

		rigidbody2d.velocity = new Vector2 (0f, 0f);
		rigidbody2d.AddForce(new Vector2(0f, jumpHeight));

	}

	void OnDrawGizmos() {

		if (groundCheck != null) {

			foreach (Transform t in groundCheck) {

				Gizmos.DrawSphere (t.position, groundRadius);

			}

		}

	}
}
