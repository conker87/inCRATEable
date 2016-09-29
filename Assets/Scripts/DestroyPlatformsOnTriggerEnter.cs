using UnityEngine;
using System.Collections;

public class DestroyPlatformsOnTriggerEnter : MonoBehaviour {

	public string platformTag;

	void OnTriggerEnter2D(Collider2D other) {

		Debug.Log (other.transform.parent.name + ", tag: " + other.transform.parent.tag + ", pos: " + other.transform.parent.position);

		if (other.transform.parent.tag == platformTag) {

			Destroy (other.transform.parent.gameObject);

		}

	}

}
