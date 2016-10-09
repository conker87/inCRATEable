using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetAcceleratorDeadzone : MonoBehaviour {

	public string prefix, suffix, format;

	Text text;

	// Use this for initialization
	void Start () {
	
		text = GetComponent<Text> ();

	}
	
	// Update is called once per frame
	void Update () {
	
		text.text = prefix + " " + GameManager.instance.accelerationDeadzone.ToString(format) + " " + suffix;

	}
}
