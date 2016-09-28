using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SmoothCamera2D : MonoBehaviour {

	public float dampTime = 0.15f, lockXAt = 0f, restrictYAbove = 0f;
	private Vector3 velocity = Vector3.zero, position;
	public Transform target;

	void Start() {

		target = GameObject.FindGameObjectWithTag ("Player").transform;

	}

	// Update is called once per frame
	void Update () {

		FindPlayerIfNull ();

		if (target) {
			
			Vector3 point = Camera.main.WorldToViewportPoint(target.position);
			Vector3 delta = target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);

			position = new Vector3 (lockXAt, Mathf.Clamp(position.y, restrictYAbove, Mathf.Infinity), -10f);

			transform.position = position;

		}

	}

	void FindPlayerIfNull() {

		if (target == null) {

			target = GameObject.FindGameObjectWithTag ("Player").transform;

			if (target == null) {

				Debug.LogError ("Player cannot be found a second time, there is something seriously wrong!");

			}

		}

	}
}