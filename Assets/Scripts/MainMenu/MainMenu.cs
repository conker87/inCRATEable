using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public void LoadScene(string SceneName = "MainMenu") {

		SceneManager.LoadScene (SceneName);

	}

	public void About() {



	}

	public void Quit() {

		Application.Quit ();

	}

}
