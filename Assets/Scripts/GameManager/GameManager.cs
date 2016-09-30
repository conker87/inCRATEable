using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	public float score, topScore;

	GameObject target;

	void Awake() {

		Singleton ();

	}
		
	void Start () {
	
		FindPlayer();

	}

	void Update () {
	
		if (FindPlayer()) {

			SetScore ();

		}
			
	}

	void SetScore() {

		score = target.transform.position.y;

		if (score > topScore) {

			topScore = score;

		}

	}

	public float GetScore() {

		return topScore;

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

				Debug.LogError ("Player cannot be found a second time, there is something seriously wrong!");

				return false;

			}

			return true;

		} else {

			return true;

		}

	}

}
