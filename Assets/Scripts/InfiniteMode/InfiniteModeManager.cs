using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class InfiniteModeManager : MonoBehaviour {

	public static InfiniteModeManager instance = null;

	[Header("Manager Settings")]
	[Header("Player")]
	public GameObject playerPrefab;
	public Vector3 targetStartingPosition;
	GameObject target;

	[Header("Canvases / UI")]
	public GameObject GameOverParent;
	public GameObject PauseParent, TimerParent;
	public Text ScoringLocation;

	[Header("Timer")]
	public float timerStart = 3f;
	public float currentTimer;
	bool startGame = true;

	float startScoringAtPosition = 20f, scoringMultiplier = 10f, currentScore;

	AudioSource audioSource;

	void Awake() {

		Singleton ();

		audioSource = GetComponent<AudioSource> ();

	}

	void Start() {

		// Reset pausing mechanic.
		GameManager.instance.gameOver = false;
		GameOverParent.SetActive (false);

		GameManager.instance.paused = true;
		PauseParent.SetActive (false);

		// Reset score.
		GameManager.instance.maxScore = 0f;

		// Spawn player
		target = Instantiate (playerPrefab, targetStartingPosition, Quaternion.identity) as GameObject;

		// Start timer.
		currentTimer = timerStart;
		TimerParent.SetActive (true);
	}

	void Update() {

		if (GameManager.instance.music && !audioSource.isPlaying) {

			audioSource.Play ();

		}

		if (!GameManager.instance.gameOver) {

			if (startGame) {

				if (currentTimer > 0f) { 
					currentTimer -= Time.deltaTime;
				}

				if (currentTimer <= 0f) {

					startGame = false;
					TimerParent.SetActive (false);
					GameManager.instance.paused = false;

					currentTimer = 1f;

				}

				if (GameManager.instance.difficulty > 1) {
					CheckForAchievements ();
				}

			}

			scoringMultiplier = GameManager.instance.ScoreMultiplier;
			SetScore ();

		} else {

			if (currentTimer > 0f) { 

				TimerParent.SetActive (true);
				currentTimer -= Time.deltaTime;

			}

			if (currentTimer <= 0f) {

				TimerParent.SetActive (false);

				if (!GameOverParent.activeSelf) {

					if (Social.localUser.authenticated && GameManager.instance.difficulty > 1) {

						string id;

						ScoringLocation.text = "Score: " + GameManager.instance.GetScore ().ToString ();

						if (GameManager.instance.infiniteMode_Brutal) {
							
							id = GameManager.instance.Leaderboard_InfiniteModeBrutalMode;

						} else {

							id = GameManager.instance.Leaderboard_InfiniteModeNormal;

							if (GameManager.instance.difficulty == 3) {
								
								id = GameManager.instance.Leaderboard_InfiniteModeHard;

							}

							if (GameManager.instance.difficulty == 4) {
								
								id = GameManager.instance.Leaderboard_InfiniteModeBrutal;

							}
							
						}

						Social.ReportScore (long.Parse (GameManager.instance.GetScore().ToString()), id,

							(bool success) => {

								if (success) {

									ScoringLocation.text = "Score sent!";

								} else {

									ScoringLocation.text = "Score sending failed :(";

								}

							});

					} else {

						ScoringLocation.text = "Local Score: " + GameManager.instance.GetScore ().ToString ();

					}

					if (Random.value < 0.9f && Advertisement.IsReady ()) {

						Advertisement.Show ();

					}

				}

				GameOverParent.SetActive (true);

			}

		}

		ReloadSceneOnKeypress (KeyCode.P);

	}

	void CheckForAchievements () {

		if (GameManager.instance.GetScore () > 10000f) {

			Social.ReportProgress (GameManager.instance.Achievement_InfiniteMode_10000, 100.0f, (bool success) =>
				{
					if (success)
					{
						Debug.Log("Achievement giving success");
					}
					else
					{
						Debug.Log("Achievement giving failed");
					}
				});

		}

		if (GameManager.instance.GetScore () > 20000f) {

			Social.ReportProgress (GameManager.instance.Achievement_InfiniteMode_20000, 100.0f, (bool success) =>
				{
					if (success)
					{
						Debug.Log("Achievement giving success");
					}
					else
					{
						Debug.Log("Achievement giving failed");
					}
				});

		}

		if (GameManager.instance.GetScore () > 30000f) {

			Social.ReportProgress (GameManager.instance.Achievement_InfiniteMode_30000, 100.0f, (bool success) =>
				{
					if (success)
					{
						Debug.Log("Achievement giving success");
					}
					else
					{
						Debug.Log("Achievement giving failed");
					}
				});

		}

		if (GameManager.instance.GetScore () > 50000f) {

			Social.ReportProgress (GameManager.instance.Achievement_InfiniteMode_50000, 100.0f, (bool success) =>
				{
					if (success)
					{
						Debug.Log("Achievement giving success");
					}
					else
					{
						Debug.Log("Achievement giving failed");
					}
				});

		}

		if (GameManager.instance.GetScore () > 100000f) {

			Social.ReportProgress (GameManager.instance.Achievement_InfiniteMode_100000, 100.0f, (bool success) =>
				{
					if (success)
					{
						Debug.Log("Achievement giving success");
					}
					else
					{
						Debug.Log("Achievement giving failed");
					}
				});

		}

		if (GameManager.instance.GetScore () > 500000f) {

			Social.ReportProgress (GameManager.instance.Achievement_InfiniteMode_500000, 100.0f, (bool success) =>
				{
					if (success)
					{
						Debug.Log("Achievement giving success");
					}
					else
					{
						Debug.Log("Achievement giving failed");
					}
				});

		}

		if (GameManager.instance.GetScore () > 1000000f) {

			Social.ReportProgress (GameManager.instance.Achievement_InfiniteMode_1000000, 100.0f, (bool success) =>
				{
					if (success)
					{
						Debug.Log("Achievement giving success");
					}
					else
					{
						Debug.Log("Achievement giving failed");
					}
				});

		}

	}

	public void TESTING_ADD_SCORE(float score) {

		GameManager.instance.SetScore (score + GameManager.instance.GetScore ());

	}

	void SetScore() {

		if (target.transform.position.y > startScoringAtPosition) {

			currentScore = Mathf.Round((target.transform.position.y - startScoringAtPosition) * scoringMultiplier);

			if (currentScore > GameManager.instance.GetScore()) {

				GameManager.instance.SetScore(currentScore);

			}

		}

	}

	public void Restart() {

		SceneManager.LoadScene ("InfiniteMode");

	}

	public void TogglePause() {

		if (PauseParent != null) {

			PauseParent.SetActive (!PauseParent.activeInHierarchy);
			GameManager.instance.paused = PauseParent.activeInHierarchy;

		}

	}

	public void QuitToMainMenu() {

		// Save score?
		SceneManager.LoadScene ("MainMenu");

	}

	void ReloadSceneOnKeypress (KeyCode key) {

		if (Input.GetKeyUp (key)) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}

	}

	bool FindPlayer() {

		if (target == null) {

			target = GameObject.FindGameObjectWithTag ("Player");

			if (target == null) {

				if (target == null) {

					Debug.LogError ("Player cannot be found a second time, there is something seriously wrong!");

					return false;

				}

			}

			return true;

		} else {

			return true;

		}

	}

	void Singleton() {

		if (instance == null) {

			instance = this;

		} else if (instance != this) {

			Destroy (this);

		}
			
	}

}
