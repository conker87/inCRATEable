using GooglePlayGames;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.Audio;
using UnityEngine.UI;
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
	public Slider AcceleratorDeadzone, VolumeMaster, VolumeMusic, VolumeEffects;
	public Toggle Music;

	[Header("Details Parent")]
	public GameObject[] allDetails;

	AudioSource audioSource;
	bool beginMusic = false;

	void Start() {

		GameManager.instance.gameOver = GameManager.instance.paused = false;

		ResetUIElements ();

		audioSource = GetComponent<AudioSource> ();

	}

	void Update() {

		if (GameManager.instance.music && !audioSource.isPlaying) {

			audioSource.Play ();

		}
		if (!GameManager.instance.music) {

			audioSource.Pause ();

		}

		if (Social.localUser.authenticated) {



		} else {



		}

	}

	public void LoadScene(string SceneName = "MainMenu") {

		SceneManager.LoadScene (SceneName);

	}

	public void Login(bool justLogin = false) {

		if (Social.localUser.authenticated) {



		} else if (justLogin || !Social.localUser.authenticated) {
			
			GameManager.instance.authenticating = true;

			Social.localUser.Authenticate ((bool success) => {

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

	public void Logout(bool forced = false) {

		if (forced || Social.localUser.authenticated) {

			((PlayGamesPlatform) Social.Active).SignOut();

		}

	}

	void ResetUIElements() {

		InfiniteMode_Brutal.isOn = GameManager.instance.infiniteMode_Brutal;
		InfiniteMode_Rainbow.isOn = GameManager.instance.infiniteMode_Rainbow;
		Difficulty.value = GameManager.instance.difficulty;
		Music.isOn = GameManager.instance.music;

	}

	public void ShowLeaderboard(string leaderboard = "") {

		string lb;

		if (leaderboard == "") {
		
			Social.ShowLeaderboardUI();
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

	public void Settings_ToggleMusic() {

		GameManager.instance.music = Music.isOn;

	}

	public void Settings_ChangeVolumeMaster(AudioMixerGroup mixer) {

		//Settings_ChangeVolumeMaster.value;

	}

	public void Settings_ResetSettingsToDefault() {

		AcceleratorDeadzone.value = 0.0f;
		AcceleratorSensitivity.value = 1.0f;

	}

	public void Quit() {

		Application.Quit ();

	}

}
