using UnityEngine;
using System.Collections;

public class Jump2D : MonoBehaviour {

	[SerializeField]
	bool grounded;
	[SerializeField]
	Collider2D groundHit;
	JumpPlatform2D jp;

	public float jumpHeight = 500f, originalJumpHeight;
	public Transform groundCheck;

	public float groundRadius = .2f;
	public LayerMask grounds;

	float velocityY;

	Rigidbody2D rigidbody2d;

	void Start () {

		rigidbody2d = GetComponent<Rigidbody2D> ();

		originalJumpHeight = jumpHeight;
	}

	void FixedUpdate () {
	
		grounded = groundHit = Physics2D.OverlapCircle(groundCheck.position, groundRadius, grounds);

		if (groundHit != null && (jp = groundHit.transform.parent.GetComponent<JumpPlatform2D> ()) != null) {

			Debug.Log (groundHit);

			jumpHeight = jp.jumpHeight;

		}

		velocityY = rigidbody2d.velocity.y;

		if (grounded && velocityY <= 0) {

			Jump (jumpHeight);

			jumpHeight = originalJumpHeight;

		}

	}

	void Jump(float jumpHeight) {

		rigidbody2d.velocity = new Vector2 (0f, 0f);
		rigidbody2d.AddForce(new Vector2(0f, jumpHeight));

	}



	void OnDrawGizmos() {

		Gizmos.DrawSphere (groundCheck.position, groundRadius);

	}
}
