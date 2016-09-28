﻿using UnityEngine;
using System.Collections;

public class LeftRight2D : MonoBehaviour {

	[Header("Movement & Speed")]
	public float platformSpeed = 2f;
	public StartingDirectionLeftRight d;

	[Header("Position Restrictions")]
	[Range(-15, 15)] public float minimum = -5f;
	[Range(-15, 15)] public float maximum = 5f;




	bool right;

	// Use this for initialization
	void Start () {
	
		right = (d == StartingDirectionLeftRight.LEFT) ? right = false : right = true;

	}
	
	// Update is called once per frame
	void Update () {

		ValidateUserInput ();
	
		CheckPosition ();

		MovePlatform ();

	}

	void CheckPosition() {

		if (transform.position.x < minimum) {

			right = true;

		}

		if (transform.position.x > maximum) {

			right = false;

		}

	}

	void MovePlatform() {

		if (right) {

			transform.position += Vector3.right * platformSpeed * Time.deltaTime;

		}

		if (!right) {

			transform.position += Vector3.left * platformSpeed * Time.deltaTime;

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
	
public enum StartingDirectionLeftRight { LEFT, RIGHT };