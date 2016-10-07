using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowUsernameIfLoggedIn : MonoBehaviour {

	Text text;

	void Start() {

		text = GetComponent<Text> ();

	}

	void Update () {
	
		if (Social.localUser.authenticated) {

			text.text = "Welcome " + Social.localUser.userName + "!";

		} else {

			if (GameManager.instance.authenticating) {

				text.text = "Authenticating...";

			} else {

				text.text = "Not logged in.";

			}

		}

	}

}
