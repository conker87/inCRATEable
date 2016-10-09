using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetScore : MonoBehaviour {

	Text text;

	void Start () {

		text = GetComponent<Text> ();

	}


	void Update () {

		text.text = "Score " + string.Format("{0:n0}", GameManager.instance.GetScore ());

	}

}