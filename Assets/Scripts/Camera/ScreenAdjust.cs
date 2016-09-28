using UnityEngine;
using System.Collections;

[ExecuteInEditMode]	
public class ScreenAdjust : MonoBehaviour {

	[Range(10, 2560)]
	public float targetHeight = 1024f, targetWidth = 600f;

	public bool useUpdate = false;

	void Start () {
	
		Camera.main.aspect = targetWidth / targetHeight;

	}

	void Update() {

		if (useUpdate) {

			Camera.main.aspect = targetWidth / targetHeight;

		}

	}
}
