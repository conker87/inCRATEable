using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetScore : MonoBehaviour {

	void Update () {

		GetComponent<Text> ().text = "Score " + string.Format("{0:n0}", GameManager.instance.GetScore ());

	}

}
