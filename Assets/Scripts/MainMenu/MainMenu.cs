using GooglePlayGames;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using System.Collections;

public class MainMenu : MonoBehaviour {

	[Header("Canvases")]
	public GameObject AboutParent;
	public GameObject SettingsParent;

	[Header("Buttons")]
	public Button AboutButton;
	public Button InfiniteModeButton, LoginButton, QuitButton, SettingsButton;

	public void LoadScene(string SceneName = "MainMenu") {

		SceneManager.LoadScene (SceneName);

	}

	void Start() {

		GameManager.instance.gameOver = GameManager.instance.paused = false;
		GameManager.instance.currentState = "MainMenu";

		Login ();

	}

	void Update() {
		
		if (Social.localUser.authenticated) {

			Debug.Log ("Logged in.");

		} else {

			Debug.Log ("Logged out.");

		}

	}

	public void Login() {

		if (Social.localUser.authenticated) {

			((PlayGamesPlatform)Social.Active).SignOut ();

		} else {
			
			LoginButton.GetComponentInChildren<Text> ().text = "Login";

			Social.localUser.Authenticate ((bool success) => {
				
				if (success) {

					Debug.Log ("You've successfully logged in");
					LoginButton.GetComponentInChildren<Text>().text = "Logout";

				} else {
					
					Debug.Log ("Login failed for some reason");
					LoginButton.GetComponentInChildren<Text>().text = "Failed";

				}

			});

		}
		
	}

	public void ToggleGameObject(GameObject toggle) {

		if (toggle != null) {

			toggle.SetActive(!toggle.activeSelf);
			setButtons(!toggle.activeSelf);

		}

	}

	public void ToggleSettings() {

		if (SettingsParent != null) {

			SettingsParent.SetActive(!SettingsParent.activeSelf);
			setButtons(!SettingsParent.activeSelf);

		}

	}

	public void ToggleAbout() {

		if (AboutParent != null) {

			AboutParent.SetActive(!AboutParent.activeSelf);
			setButtons(!AboutParent.activeSelf);

		}

	}

	void setButtons(bool value) {

		AboutButton.interactable = InfiniteModeButton.interactable = QuitButton.interactable = SettingsButton.interactable = value;

	}

	public void Quit() {

		Application.Quit ();

	}

}
