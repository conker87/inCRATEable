using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SmoothCamera2D : MonoBehaviour {

	public float dampTime = 0.15f, lockXAt = 0f, restrictYAbove = 0f, currentMaxYLevel;
	private Vector3 velocity = Vector3.zero, position;
	public GameObject target;

	void Start() {

		FindPlayer ();

		currentMaxYLevel = restrictYAbove;

	}

	// Update is called once per frame
	void Update () {

		if (FindPlayer ()) {
			
			Vector3 point = Camera.main.WorldToViewportPoint(target.transform.position);
			Vector3 delta = target.transform.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;

			if (transform.position.y - 1f > currentMaxYLevel) {

				currentMaxYLevel = transform.position.y - 1f;

			}

			position = Vector3.SmoothDamp (transform.position, destination, ref velocity, dampTime);

			position = new Vector3 (lockXAt, Mathf.Clamp(position.y, currentMaxYLevel, Mathf.Infinity), -10f);

			transform.position = position;

		}

	}

	bool FindPlayer() {

		if (target == null) {

			target = GameObject.FindGameObjectWithTag ("Player");

			if (target == null) {

				Debug.LogError ("Player cannot be found a second time, there is something seriously wrong!");

				return false;

			}

			return true;

		} else {

			return true;

		}

	}

}