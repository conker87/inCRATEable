using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

	[Header("Canvases")]
	public GameObject AboutParent;
	public GameObject SettingsParent;

	[Header("Buttons")]
	public Button AboutButton;
	public Button InfiniteModeButton, QuitButton, SettingsButton;

	public void LoadScene(string SceneName = "MainMenu") {

		SceneManager.LoadScene (SceneName);

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
