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
	public Dropdown Difficulty;

	[Header("Settings Sliders")]
	public Slider AcceleratorSensitivity;
	public Slider AcceleratorDeadzone;

	[Header("Details Parent")]
	public GameObject[] allDetails;

	void Start() {

		GameManager.instance.gameOver = GameManager.instance.paused = false;

		ResetUIElements ();

	}

	void Update() {

		if (Social.localUser.authenticated) {



		} else {



		}

	}

	public void LoadScene(string SceneName = "MainMenu") {

		SceneManager.LoadScene (SceneName);

	}

	public void Login(bool justLogin = false) {

		if (!justLogin && Social.localUser.authenticated) {

			((PlayGamesPlatform)Social.Active).SignOut ();

		} else if (justLogin || !Social.localUser.authenticated) {

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
		Difficulty.value = GameManager.instance.difficulty;

	}

	public void ShowLeaderboard(string leaderboard = "") {

		string lb;

		if (leaderboard == "") {
		
			return;

		} else {

			lb = leaderboard;

		}

		((PlayGamesPlatform) Social.Active).ShowLeaderboardUI (lb);

	}

	public void ShowAchievementsUI() {

		Social.ShowAchievementsUI();

	}

	public void InfiniteMode_ToggleBrutalMode(Toggle toggle) {

		GameManager.instance.infiniteMode_Brutal = toggle.isOn;

		if (GameManager.instance.infiniteMode_Brutal) {

			Difficulty.value = 4;
			Difficulty.interactable = false;

		} else {

			Difficulty.interactable = true;

		}

	}

	public void InfiniteMode_ToggleRainbowMode(Toggle toggle) {

		GameManager.instance.infiniteMode_Rainbow = toggle.isOn;

	}

	public void InfiniteMode_DifficultyChanged() {

		GameManager.instance.difficulty = Difficulty.value;

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

	public void Settings_ChangeAcceleratorDeadzone() {

		GameManager.instance.accelerationDeadzone = AcceleratorDeadzone.value;

	}

	public void Settings_ChangeAcceleratorSensitivity() {

		GameManager.instance.acceleratorSensitivity = AcceleratorSensitivity.value;

	}

	public void Settings_ResetSettingsToDefault() {

		AcceleratorDeadzone.value = 0.0f;
		AcceleratorSensitivity.value = 1.0f;


	}

	public void Quit() {

		Application.Quit ();

	}

}
