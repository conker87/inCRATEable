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
	public int difficulty = 1;
	public float ScoreMultiplier = 10f;

	public float acceleratorSensitivity = 1f, accelerationDeadzone = 0f;

	public readonly string Leaderboard_InfiniteModeNormal = "CgkI7eiV49sPEAIQBg", Leaderboard_InfiniteModeHard = "CgkI7eiV49sPEAIQCA", Leaderboard_InfiniteModeBrutal = "CgkI7eiV49sPEAIQCQ",
								Leaderboard_InfiniteModeBrutalMode = "CgkI7eiV49sPEAIQBw";
	public readonly string Achievement_InfiniteMode_10000 = "CgkI7eiV49sPEAIQAQ", Achievement_InfiniteMode_20000 = "CgkI7eiV49sPEAIQAg", Achievement_InfiniteMode_30000 = "CgkI7eiV49sPEAIQAw", 
							Achievement_InfiniteMode_50000 = "CgkI7eiV49sPEAIQBA", Achievement_InfiniteMode_100000 = "CgkI7eiV49sPEAIQBQ", Achievement_InfiniteMode_500000 = "CgkI7eiV49sPEAIQCg", 
							Achievement_InfiniteMode_1000000 = "CgkI7eiV49sPEAIQCw";

	GameObject target;

	void Awake() {

		GooglePlayGames.PlayGamesPlatform.Activate();

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
