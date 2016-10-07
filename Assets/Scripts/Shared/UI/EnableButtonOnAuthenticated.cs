using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnableButtonOnAuthenticated : MonoBehaviour {

	Button thisButton;

	void Start() {

		thisButton = GetComponent<Button> ();

	}

	void Update () {
		
		if (Social.localUser.authenticated) {

			thisButton.interactable = true;

		} else {

			thisButton.interactable = false;

		}

	}

}
