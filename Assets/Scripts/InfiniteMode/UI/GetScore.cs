using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetScore : MonoBehaviour {

	void Update () {
	
		GetComponent<Text> ().text = "Score " + GameManager.instance.GetScore ();

	}

}
