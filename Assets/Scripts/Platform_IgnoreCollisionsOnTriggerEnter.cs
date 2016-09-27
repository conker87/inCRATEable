using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Platform_IgnoreCollisionsOnTriggerEnter : MonoBehaviour {

	public string playerTag, originalName;
	BoxCollider2D[] children;

	void Start() {

		children = GetComponentsInChildren<BoxCollider2D> ();

	}

	void OnTriggerEnter2D(Collider2D other) {

		Debug.Log (other.tag);

		if (other.tag == playerTag) {

			ItterateThroughChildren (true);

		}

	}

	void OnTriggerStay2D(Collider2D other) {

		Debug.Log (other.tag);

		if (other.tag == playerTag) {

			ItterateThroughChildren (true);

		}

	}

	void OnTriggerExit2D(Collider2D other) {

		Debug.Log (other.tag);

		if (other.tag == playerTag) {

			ItterateThroughChildren (false);

		}

	}

	void ItterateThroughChildren(bool SetTrigger = false) {

		for (int i = 0; i < children.Length; i++) {

			if (children [i].gameObject.name == originalName + children [i].gameObject.GetInstanceID ()) {

				continue;

			}

			children [i].isTrigger = SetTrigger;

		}

	}

}
