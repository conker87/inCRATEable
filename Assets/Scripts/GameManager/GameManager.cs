using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	public float currentScore, maximumInstanceScore;

	public bool gameOver = false, paused = false;

	float startScoringAtPosition = 20f, scoringMultiplier = 10f;

	GameObject target;

	void Awake() {

		Singleton ();

	}
		
	void Start () {
	
		FindPlayer();

	}

	void Update () {
	
		if (FindPlayer()) {

			if (GameManager.instance.paused || GameManager.instance.gameOver) {



			} else {

				SetScore ();

			}

		}
			
	}

	void SetScore() {

		if (target.transform.position.y > startScoringAtPosition) {

			currentScore = Mathf.Round((target.transform.position.y - startScoringAtPosition) * scoringMultiplier);

			if (currentScore > maximumInstanceScore) {

				maximumInstanceScore = currentScore;

			}

		}

	}

	public float GetScore() {

		return maximumInstanceScore;

	}

	void Singleton() {
		
		if (instance == null) {
			
			instance = this;

		} else if (instance != this) {

			Destroy (this);

		}

		DontDestroyOnLoad (gameObject);

	}

	bool FindPlayer() {

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
