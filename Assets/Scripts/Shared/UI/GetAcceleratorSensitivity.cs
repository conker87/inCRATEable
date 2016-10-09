using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetAcceleratorSensitivity : MonoBehaviour {

	public string prefix, suffix, format;

	Text text;

	// Use this for initialization
	void Start () {
	
		text = GetComponent<Text> ();

	}
	
	// Update is called once per frame
	void Update () {
	
		text.text = prefix + " " + GameManager.instance.acceleratorSensitivity.ToString(format) + " " + suffix;

	}
}
