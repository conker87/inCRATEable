using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IgnoreCollisionsOnTriggerEnter : MonoBehaviour {

	public string playerTag;
	BoxCollider2D[] children;

	void Start() {

		children = transform.parent.GetComponentsInChildren<BoxCollider2D> ();

	}

	void OnTriggerEnter2D(Collider2D other) {

		//Debug.Log (other.name + other.GetInstanceID() + ", tag: " + other.tag + ", pos: " + other.transform.position);

		if (other.tag == playerTag) {

			ItterateThroughChildren (true);

		}

	}

	void OnTriggerStay2D(Collider2D other) {

		//Debug.Log (other.name + other.GetInstanceID() + ", tag: " + other.tag + ", pos: " + other.transform.position);

		if (other.tag == playerTag) {

			ItterateThroughChildren (true);

		}

	}

	void OnTriggerExit2D(Collider2D other) {

		//Debug.Log (other.name + other.GetInstanceID() + ", tag: " + other.tag + ", pos: " + other.transform.position);

		if (other.tag == playerTag) {

			ItterateThroughChildren (false);

		}

	}

	void ItterateThroughChildren(bool SetTrigger = false) {

		for (int i = 0; i < children.Length; i++) {

			//Debug.Log (children[i].name);

			if (children[i].gameObject.name == "RandomPlatformParent") {

				continue;

			}

			children [i].isTrigger = SetTrigger;

		}

	}

}
