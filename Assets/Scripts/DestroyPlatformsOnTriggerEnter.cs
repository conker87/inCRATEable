using UnityEngine;
using System.Collections;

public class DestroyPlatformsOnTriggerEnter : MonoBehaviour {

	public string platformTag;

	void OnTriggerEnter2D(Collider2D other) {

		//Debug.Log (other.name + ", tag: " + other.tag + ", pos: " + other.transform.position);

		if (other.tag == platformTag) {

			Destroy (other.gameObject);

		}

	}

}
