using GooglePlayGames;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	public float currentScore, maxScore;

	public string currentState = "MainMenu";

	public bool gameOver = false, paused = false, authenticating = false;

	public bool infiniteMode_Brutal = false, infiniteMode_Rainbow = false;



	public readonly string Leaderboard_InfiniteMode = "CgkI7eiV49sPEAIQBg", Leaderboard_InfiniteModeBrutal = "CgkI7eiV49sPEAIQBw";
	public readonly string Achievement_1 = "";

	GameObject target;

	void Awake() {

		Singleton ();

	}
		
	void Start () {
	
		FindPlayer();

		PlayGamesPlatform.Activate();

	}

	void Update () {
	
		if (FindPlayer ()) {

			if (GameManager.instance.paused || GameManager.instance.gameOver) {



			} else {



			}

		} else {

			GameManager.instance.gameOver = true;

		}
			
	}

	public void SetScore(float score) {

		maxScore = score;

	}

	public float GetScore() {

		return maxScore;

	}

	void Singleton() {
		
		if (instance == null) {
			
			instance = this;

		} else if (instance != this) {

			Destroy (this);

		}

		DontDestroyOnLoad (gameObject);

	}

	public bool FindPlayer() {

		if (target == null) {

			target = GameObject.FindGameObjectWithTag ("Player");

			if (target == null) {

				if (target == null) {

					Debug.LogError ("Player cannot be found a second time, there is something seriously wrong! Unless you're on the Main Menu then this is normal.");

					return false;

				}

			}

			return true;

		} else {

			return true;

		}

	}

}
