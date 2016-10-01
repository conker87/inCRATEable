using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class InfiniteModeManager : MonoBehaviour {

	public GameObject GameOverParent, PauseParent;

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


}
