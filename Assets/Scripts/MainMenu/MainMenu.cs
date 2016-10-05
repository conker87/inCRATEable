using GooglePlayGames;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using System.Collections;

public class MainMenu : MonoBehaviour {

	[Header("Game Buttons")]
	public Button InfiniteModeButton;
	public Button LeaderboardButton, SettingsButton, QuitButton;

	[Header("Login Buttons")]
	public Button LoginButton;
	public Button LogoutButton;

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

			Social.localUser.Authenticate ((bool success) => {
				
				if (success) {

					Debug.Log ("You've successfully logged in");

				} else {
					
					Debug.Log ("Login failed for some reason");

				}

			});

		}
		
	}

	public void ShowLeaderboard(string leaderboard) {

		((PlayGamesPlatform)Social.Active).ShowLeaderboardUI (GameManager.instance.Leaderboard_InfiniteMode);

	}

	public void ToggleDetails(GameObject toggle) {

			if (toggle != null) {

				toggle.SetActive (!toggle.activeSelf);
				setButtons (!toggle.activeSelf);

			}

	}

	void setButtons(bool value) {

		//AboutButton.interactable = InfiniteModeButton.interactable = QuitButton.interactable = SettingsButton.interactable = value;

	}

	public void Quit() {

		Application.Quit ();

	}

}
