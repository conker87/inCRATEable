using UnityEngine;
using System.Collections;

public class Jump2D : MonoBehaviour {

	[SerializeField]
	bool grounded;
	public float jumpHeight = 500f;
	public Transform groundCheck;

	public float groundRadius = .2f;
	public LayerMask grounds;

	float velocityY;

	Rigidbody2D rigidbody2d;

	void Start () {

		rigidbody2d = GetComponent<Rigidbody2D> ();

		}

	void FixedUpdate () {
	
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, grounds);

		velocityY = rigidbody2d.velocity.y;

		if (grounded && velocityY <= 0) {

			Jump ();

		}

	}

	void Jump() {

		rigidbody2d.velocity = new Vector2 (0f, 0f);
		rigidbody2d.AddForce(new Vector2(0f, jumpHeight));

	}



	void OnDrawGizmos() {

		Gizmos.DrawSphere (groundCheck.position, groundRadius);

	}
}
