using UnityEngine;
using System.Collections;

public class RainbowModeChild : MonoBehaviour {

	public Color setColor;

	// Use this for initialization
	void Start () {
	
		GetComponent<SpriteRenderer> ().color = setColor;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
