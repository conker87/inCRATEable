using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using System.Collections;

public class InfiniteModeManager : MonoBehaviour {

	public static InfiniteModeManager instance = null;

	[Header("Player")]
	public GameObject target;
	public Vector3 targetStartingPosition;

	[Header("Canvases")]
	public GameObject GameOverParent;
	public GameObject PauseParent;
	public GameObject TimerParent;

	[Header("Timer")]
	public float timerStart = 3f;
	public float currentTimer;
	bool startGame = true;

	void Awake() {

		Singleton ();

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
		Instantiate (target, targetStartingPosition, Quaternion.identity);

		// Start timer.
		currentTimer = timerStart;
		TimerParent.SetActive (true);
	}

	void Update() {

		if (!GameManager.instance.gameOver) {

			if (startGame) {

				//Debug.Log (currentTimer);
				currentTimer -= Time.deltaTime;

				if (currentTimer < 0f) {

					startGame = false;
					TimerParent.SetActive (false);
					GameManager.instance.paused = false;

					currentTimer = 1f;

				}

			}

		} else {

			currentTimer -= Time.deltaTime;

			if (currentTimer > 0f) { 

				TimerParent.SetActive (true);

			}

			if (currentTimer < 0f) {

				TimerParent.SetActive (false);

				if (Advertisement.IsReady () && !GameOverParent.activeSelf) {

					Advertisement.Show ();

				}

			}

		}

		ReloadSceneOnKeypress (KeyCode.P);

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
