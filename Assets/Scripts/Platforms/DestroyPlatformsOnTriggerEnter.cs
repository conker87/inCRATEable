using UnityEngine;
using System.Collections;

public class DestroyPlatformsOnTriggerEnter : MonoBehaviour {

	public string platformTag;

	void OnTriggerEnter2D(Collider2D other) {

		if (other.transform.parent != null && other.transform.parent.tag == platformTag) {

			Destroy (other.transform.parent.gameObject);

		}

	}

}
