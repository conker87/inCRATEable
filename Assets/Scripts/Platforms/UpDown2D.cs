using UnityEngine;
using System.Collections;

public class UpDown2D : MonoBehaviour {

	[Header("Movement & Speed")]
	public float platformSpeed = 2f;
	public StartingDirectionUpDown d;

	[Header("Position Restrictions")]
	public float minimum = 10f;
	public float maximum = 15f;

	bool up;

	// Use this for initialization
	void Start () {
	
		up = (d == StartingDirectionUpDown.DOWN) ? up = false : up = true;

	}
	
	// Update is called once per frame
	void Update () {

		ValidateUserInput ();
	
		CheckPosition ();

		MovePlatform ();

	}

	void CheckPosition() {

		if (transform.position.y < minimum) {

			up = true;

		}

		if (transform.position.y > maximum) {

			up = false;

		}

	}

	void MovePlatform() {

		if (up) {

			transform.position += Vector3.up * platformSpeed * Time.deltaTime;

		}

		if (!up) {

			transform.position += Vector3.down * platformSpeed * Time.deltaTime;

		}

	}

	void ValidateUserInput() {

		float temp;

		if (minimum > maximum) {

			temp = maximum;

			maximum = minimum;
			minimum = temp;

		}

	}

}

public enum StartingDirectionUpDown { UP, DOWN };