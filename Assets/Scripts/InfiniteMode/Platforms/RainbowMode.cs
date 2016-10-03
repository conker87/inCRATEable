using UnityEngine;
using System.Collections;

public class RainbowMode : MonoBehaviour {

	Color[] colorsToPick = new Color[] { Color.white, Color.red, new Color(255f / 255f, 165f / 255f, 0f), Color.yellow, Color.green, Color.blue, new Color(75f / 255f, 0f , 130f / 255f), new Color(238f / 255f, 130f / 255f, 238f / 255f) };

	SpriteRenderer[] children;

	bool done = false;

	void Start () {
	
		children = GetComponentsInChildren<SpriteRenderer> ();

	}
	
	// Update is called once per frame
	void Update () {

		Color color = colorsToPick [Random.Range (0, colorsToPick.Length)];

		if (!done) {
			
			foreach (SpriteRenderer sr in children) {

				sr.gameObject.AddComponent<RainbowModeChild> ().setColor = color;

			}

			done = true;

		}
			
	}

}
