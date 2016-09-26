using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
public class PlayerController : MonoBehaviour
{
	
	Controller2D controller;

	public float jumpHeight = 3.5f, timeToJumpApex = .4f, accelerationTimeAirbourne = .2f, accelerationTimeGrounded = .1f, moveSpeed = 20f;
	float gravity, jumpVelocity;

	Rect left, jump, right;

	[Range(1, 100)]
	public float rectHeightPercentage = 12.5f;

	float screenWidth, screenHeight, rectWidth, rectHeight;

	bool hasJumped = false;
	
	Vector3 velocity;
	float velocityXSmoothing, nextShotTime;

	void Start ()
	{
		screenWidth = Screen.width;
		screenHeight = Screen.height;

		rectWidth = screenWidth / 3;
		rectHeight = screenHeight / (100 / rectHeightPercentage);

		Debug.Log ("Width_Screen/Rect: " + screenWidth + "/" + rectWidth + ". Height_Screen/Rect: " + screenHeight + "/" + rectHeight);


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

//		if ((Input.GetButton("Jump") || Input.GetMouseButtonDown(0)) && controller.collisions.below)
//		{
//
//			velocity.y = jumpVelocity;
//
//			hasJumped = true;
//
//		}

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

		velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirbourne);
		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime);

		if (controller.collisions.below) {
		
			//velocity = Vector3.zero;

		}

	}

	void ReloadLevelWithKey(KeyCode key) {

		if (Input.GetKeyUp(KeyCode.P))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

	}

}
