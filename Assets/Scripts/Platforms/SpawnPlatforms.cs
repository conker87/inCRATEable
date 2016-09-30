using UnityEngine;
using System.Collections;

public class SpawnPlatforms : MonoBehaviour {

	public GameObject[] platformsToSpawn;
	GameObject previousPlatform, currentPlatform;

	public GameObject GeometryParent;
	public float minimumVerticalDistance = 2f, percentageChanceToSpawnPerNewPosition = 50f, timeToNextValidSpawn, minimumTimeBeforeNextValidSpawnChance = .5f;

	[SerializeField]
	float nextValidSpawnLocation;

	float startOfGameVerticalYPosition = 30f;

	void Start() {

		nextValidSpawnLocation = startOfGameVerticalYPosition + minimumVerticalDistance;

		timeToNextValidSpawn = Time.time + minimumTimeBeforeNextValidSpawnChance;

		DoStartOfGameSpawning ();

	}

	void Update() {

		if (transform.position.y > startOfGameVerticalYPosition) {
			
			InfiniteModeSpawning ();

		}

	}

	void DoStartOfGameSpawning() {

		//Do shit.

	}

	void InfiniteModeSpawning() {

		if (transform.position.y > nextValidSpawnLocation) {

			if (Time.time > timeToNextValidSpawn) {

				if (Random.value < (percentageChanceToSpawnPerNewPosition / 100)) {

					previousPlatform = (currentPlatform != null) ? currentPlatform : null;

					currentPlatform = Instantiate (platformsToSpawn [Random.Range (0, platformsToSpawn.Length)], new Vector3 (Random.Range (-7.5f, 7.5f), transform.position.y), Quaternion.identity) as GameObject;

					currentPlatform.transform.SetParent (GeometryParent.transform);

					// This makes sure that the game is not a fuckhead and you can actually get passed a hard jump.
						// TODO: This could be made better by getting the distance and multiplying by ~1100.
					if (previousPlatform != null && (currentPlatform.transform.position.y - previousPlatform.transform.position.y) > 10) {

						JumpPlatform2D previousPlatformJump = previousPlatform.AddComponent<JumpPlatform2D> ();

						previousPlatformJump.jumpHeight = 1500f;

					}

				}

				timeToNextValidSpawn = Time.time + minimumTimeBeforeNextValidSpawnChance;

			}

			nextValidSpawnLocation = transform.position.y + minimumVerticalDistance;

		}

	}

}
