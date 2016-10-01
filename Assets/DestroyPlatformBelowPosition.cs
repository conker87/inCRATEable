using UnityEngine;
using System.Collections;

public class DestroyPlatformBelowPosition : MonoBehaviour {

	[SerializeField]
	GameObject despawner;

	void Start() {

		despawner = GameObject.FindGameObjectWithTag ("GameManager_Despawner");

	}

	void Update () {
	
		if (despawner != null && transform.position.y < despawner.transform.position.y) {

			Destroy (gameObject);

		}

	}
}
