using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetTimer : MonoBehaviour {

	void Update () {

		GetComponent<Text> ().text = Mathf.Ceil(InfiniteModeManager.instance.currentTimer).ToString();

	}

}
