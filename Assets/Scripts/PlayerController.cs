using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
public class PlayerController : MonoBehaviour
{
	
	Controller2D controller;

	public float jumpHeight = 3.5f, timeToJumpApex = .4f, accelerationTimeAirbourne = .2f, accelerationTimeGrounded = .1f, moveSpeed = 50f;
	float gravity, jumpVelocity;

	Rect left, jump, right;

	[Range(1, 100)]
	public float rectHeightPercentage = 12.5f;

	float screenWidth, screenHeight, rectWidth, rectHeight, padding = 5f;

	bool hasJumped = false;
	
	Vector3 velocity;
	float velocityXSmoothing, nextShotTime;

	void Start ()
	{
		screenWidth = Screen.width;
		screenHeight = Screen.height;

		rectWidth = (screenWidth / 3) - padding;
		rectHeight = screenHeight / (100 / rectHeightPercentage);

		Debug.Log ("Width_Screen/Rect: " + screenWidth + "/" + rectWidth + ". Height_Screen/Rect: " + screenHeight + "/" + rectHeight);

		left = new Rect (0, 							0, rectWidth, rectHeight);
		jump = new Rect (rectWidth + padding, 			0, rectWidth, rectHeight);
		right = new Rect ((rectWidth * 2) + padding, 	0, rectWidth, rectHeight);

		left = new Rect (0, 				0, rectWidth, rectHeight);
		jump = new Rect (rectWidth, 		0, rectWidth, rectHeight);
		right = new Rect (rectWidth * 2, 	0, rectWidth, rectHeight);

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

		if (Input.GetMouseButtonDown (0)) {

			if (left.Contains (Input.mousePosition)) {
				Debug.Log ("Inside Left");

				velocity.x = -1f;
			}

			if (jump.Contains (Input.mousePosition) && controller.collisions.below) {
				Debug.Log ("Inside Jump");

				velocity.y = jumpVelocity;
				hasJumped = true;
			}

			if (right.Contains (Input.mousePosition)) {
				Debug.Log ("Inside Right");

				velocity.x = 1f;
			}

		}

		float targetVelocityX = velocity.x * moveSpeed;

		targetVelocityX = Mathf.Clamp (targetVelocityX, -moveSpeed, moveSpeed);

		Debug.Log ("targetVelocityX: " + targetVelocityX + ", jumpVelocity: " + jumpVelocity + ", velocity: " +velocity);

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
