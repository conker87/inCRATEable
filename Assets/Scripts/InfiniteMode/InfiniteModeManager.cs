using UnityEngine;
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

		GameManager.instance.gameOver = false;
		GameOverParent.SetActive (false);

		GameManager.instance.paused = true;
		PauseParent.SetActive (false);

		GameManager.instance.maximumInstanceScore = 0f;

		Instantiate (target, targetStartingPosition, Quaternion.identity);

		currentTimer = timerStart;
		TimerParent.SetActive (true);
	}

	void Update() {

		if (startGame) {

			//Debug.Log (currentTimer);
			currentTimer -= Time.deltaTime;

			if (currentTimer < 0f) {

				startGame = false;
				TimerParent.SetActive (false);
				GameManager.instance.paused = false;

			}

		}

		ReloadSceneOnKeypress (KeyCode.P);

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

	public void ShowGameOver() {

		if (GameOverParent != null && GameManager.instance.gameOver) {

			Debug.Log ("ShowGameOver :: UNFINISHED.");

		}

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
