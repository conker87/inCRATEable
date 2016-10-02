using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GameObject AboutParent, SettingsParent;

	public void LoadScene(string SceneName = "MainMenu") {

		SceneManager.LoadScene (SceneName);

	}

	public void ToggleGameObject(GameObject toggle) {

		if (toggle != null) {

			toggle.SetActive(!toggle.activeInHierarchy);

		}

	}

	public void ToggleSettings() {

		if (SettingsParent != null) {

			SettingsParent.SetActive(!SettingsParent.activeSelf);

		}

	}

	public void ToggleAbout() {

		if (AboutParent != null) {

			AboutParent.SetActive(!AboutParent.activeSelf);

		}

	}

	public void Quit() {

		Application.Quit ();

	}

}
