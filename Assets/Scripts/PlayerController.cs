using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
public class PlayerController : MonoBehaviour
{
	
	Controller2D controller;

	[Header("Movement & Jumping")]
	[Range(0.1f, 15f)]
	public float jumpHeight = 3.5f;
	[Range(0.01f, 3f)]
	public float 	timeToJumpApex = .4f;
	[Range(0.01f, 3f)]
	public float accelerationTimeAirbourne = .2f;
	[Range(0.01f, 3f)]
	public float accelerationTimeGrounded = .1f;

	[Range(0f, 0.3f)]
	public float accelerometerDeadzone = 0.1f;

	[Range(1f, 100f)]
	public float moveSpeed = 50f;

	float gravity, jumpVelocity;

	float screenWidth, screenHeight, padding = 5f;

	bool hasJumped = false;
	
	Vector3 velocity;
	float velocityXSmoothing, nextShotTime;

	void Start ()
	{
		screenWidth = Screen.width;
		screenHeight = Screen.height;

		controller = GetComponent<Controller2D>();

		gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

		Debug.Log ("gravity: " + gravity + ", jumpVelocity: " + jumpVelocity);

	}

	void Update ()
	{
		SetVelocityToZeroOnCollisionsAboveAndBelow ();

		ResetJumpingVarsOnCollisionBelow();

		Movement ();

		ReloadLevelWithKey (KeyCode.P);

	}

	void SetVelocityToZeroOnCollisionsAboveAndBelow() {

		if (controller.collisions.above || controller.collisions.below) 
		{
			velocity.y = 0;
		}

	}

	void ResetJumpingVarsOnCollisionBelow() {

		if (controller.collisions.below)
		{
			hasJumped = false;
		}

	}

	void Movement() {

		if (controller.collisions.below) {

			velocity.y = jumpVelocity;
			hasJumped = true;

		}

		velocity.x = (Input.acceleration.x > -accelerometerDeadzone && Input.acceleration.x < accelerometerDeadzone) ? 0f : Input.acceleration.x;

		if (Input.GetAxisRaw("Horizontal") != 0f) {

			velocity.x = Input.GetAxisRaw("Horizontal");

		}

		float targetVelocityX = velocity.x * moveSpeed;

		targetVelocityX = Mathf.Clamp (targetVelocityX, -moveSpeed, moveSpeed);

		velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirbourne);
		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime);


		if (!Input.GetMouseButton (0)) {

			velocity.x = 0f;

		}

	}

	void ReloadLevelWithKey(KeyCode key) {

		if (Input.GetKeyUp(KeyCode.P))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

	}

}
