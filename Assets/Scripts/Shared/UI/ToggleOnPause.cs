using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToggleOnPause : MonoBehaviour {

	public bool disableGameobject = false, makeUninteractable = false;
	Button thisButton;

	void Start() {

		thisButton = GetComponent<Button> ();

	}

	void Update () {
	
		if (disableGameobject) {
			
			gameObject.SetActive (!GameManager.instance.paused);

		}

		if (makeUninteractable) {

			thisButton.interactable = !GameManager.instance.paused;

		}

	}

}
