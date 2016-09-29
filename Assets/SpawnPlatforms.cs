using UnityEngine;
using System.Collections;

public class SpawnPlatforms : MonoBehaviour {

	public GameObject[] platformsToSpawn;

	public float minimumVerticalDistance = 2f, percentageChanceToSpawnPerNewPosition = 50f;

	float previousVerticalPosition, startOfGameVerticalYPosition = 30f;

	void Start() {

		previousVerticalPosition = transform.position.y;

		DoStartOfGameSpawning ();

	}

	void Update() {

		if (Mathf.Approximately (previousVerticalPosition, transform.position.y)) {



		} else {

			previousVerticalPosition = transform.position.y;

		}

	}

	void DoStartOfGameSpawning() {

		//Do shit.

	}

	void InfiniteModeSpawning() {



	}

}
