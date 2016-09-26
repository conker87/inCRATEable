using UnityEngine;
using System.Collections;

public class RectContainsInput : MonoBehaviour {

	public Controller2D player;

	Rect left, jump, right;

	[Range(1, 100)]
	public float rectHeightPercentage = 12.5f;

	float screenWidth, screenHeight, rectWidth, rectHeight;

	Vector3 velocity;

	// Use this for initialization
	void Start () {
	
		screenWidth = Screen.width;
		screenHeight = Screen.height;

		rectWidth = screenWidth / 3;
		rectHeight = screenHeight / (100 / rectHeightPercentage);

		Debug.Log ("Width_Screen/Rect: " + screenWidth + "/" + rectWidth + ". Height_Screen/Rect: " + screenHeight + "/" + rectHeight);


		left = new Rect (0, 				0, rectWidth, rectHeight);
		jump = new Rect (rectWidth, 		0, rectWidth, rectHeight);
		right = new Rect (rectWidth * 2, 	0, rectWidth, rectHeight);

	}
	
	// Update is called once per frame
	void Update () {

		if (player != null) {

			velocity = Vector3.zero;

			if (Input.GetMouseButtonDown (0)) {

				if (left.Contains (Input.mousePosition)) {
					Debug.Log ("Inside Left");
				}

				if (jump.Contains (Input.mousePosition)) {
					Debug.Log ("Inside Jump");
				}

				if (right.Contains (Input.mousePosition)) {
					Debug.Log ("Inside Right");
				}

			}

			player.Move (velocity * Time.deltaTime);

		}
	}
}
