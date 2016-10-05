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

	[Header("Infinite Mode Toggles")]
	public Toggle InfiniteMode_Brutal;
	public Toggle InfiniteMode_Rainbow;

	[Header("Details Parent")]
	public GameObject[] allDetails;

	void Start() {

		GameManager.instance.gameOver = GameManager.instance.paused = false;

		ResetUIElements ();
		Login ();

	}

	void Update() {

		Debug.Log (allDetails.ToString());

		if (Social.localUser.authenticated) {

			Debug.Log ("Logged in.");

		} else {

			Debug.Log ("Logged out.");

		}

	}

	public void LoadScene(string SceneName = "MainMenu") {

		SceneManager.LoadScene (SceneName);

	}

	public void Login() {

		if (Social.localUser.authenticated) {

			((PlayGamesPlatform)Social.Active).SignOut ();

		} else {

			Social.localUser.Authenticate ((bool success) => {

				GameManager.instance.authenticating = true;

				if (success) {

					GameManager.instance.authenticating = false;
					Debug.Log ("You've successfully logged in");

				} else {

					GameManager.instance.authenticating = false;
					Debug.Log ("Login failed for some reason");

				}

			});

		}
		
	}

	void ResetUIElements() {

		InfiniteMode_Brutal.isOn = GameManager.instance.infiniteMode_Brutal;
		InfiniteMode_Rainbow.isOn = GameManager.instance.infiniteMode_Rainbow;

	}

	public void ShowLeaderboard(string leaderboard) {

		((PlayGamesPlatform)Social.Active).ShowLeaderboardUI (GameManager.instance.Leaderboard_InfiniteMode);

	}

	public void ToggleBrutalMode(Toggle toggle) {

		GameManager.instance.infiniteMode_Brutal = toggle.isOn;

	}

	public void ToggleRainbowMode(Toggle toggle) {

		GameManager.instance.infiniteMode_Rainbow = toggle.isOn;

	}

	public void ToggleDetails(GameObject toggle) {

		foreach (GameObject t in allDetails) {

			if (t == toggle) {
				
				t.SetActive (!t.activeSelf);

			} else {
				t.SetActive (false);
			}

		}

	}

	public void Quit() {

		Application.Quit ();

	}

}
