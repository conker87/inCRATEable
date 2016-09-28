using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	public float score, topScore;

	GameObject player;

	void Awake() {

		Singleton ();

	}
		
	void Start () {
	
		player = GameObject.FindGameObjectWithTag ("Player");

	}

	void Update () {
	
		if (player == null) {

			player = GameObject.FindGameObjectWithTag ("Player");

		}

		SetScore ();

	}

	void SetScore() {

		score = player.transform.position.y;

		if (score > topScore) {

			topScore = score;

		}

//		Debug.Log (GetScore());

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
}
