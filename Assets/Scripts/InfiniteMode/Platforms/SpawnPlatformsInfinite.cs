using GooglePlayGames;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using System.Collections;

public class SpawnPlatformsInfinite : MonoBehaviour {

	public GameObject[] platformsToSpawn;
	GameObject previousPlatform, currentPlatform;

	GameObject target;

	[Header("Spawning Settings")]
	public GameObject GeometryParent;
	public GameObject Spawner;
	float startOfGameVerticalYPosition = 38f;
	public float minimumVerticalDistance = 2f, percentageChanceToSpawnPerNewPosition, maximumDistanceBetweenPlatforms = 10f;

	[Header("Infinite Mode Settings")]
	public bool enableBrutalMode = false;
	public bool enableRainbowMode = false;

	[Header("Jump Component Values")]
	public float percentageChanceToSpawnJump = 5f;
	public float minimumJumpHeightValue = 1000f;
	public float maximumJumpHeightValue = 2500f;

	[Header("Left/Right Component Values")]
	public float percentageChanceToSpawnLeftRight = 10f;
	public float minimumHorizontalValue = -5f;
	public float maximumHorizontalValue = 5f;
	public float leftRightPlatformSpeed = 2f;

	int platformSpawningInt;

	float nextValidSpawnLocation, distanceBetweenPlatforms;
	bool forceSpawnPlatform = false, beginning = true;

	void Start() {

		enableBrutalMode = GameManager.instance.infiniteMode_Brutal;
		enableRainbowMode = GameManager.instance.infiniteMode_Rainbow;

		nextValidSpawnLocation = startOfGameVerticalYPosition;

		beginning = true;

		SetDifficultySettings ();

	}

	void Update() {

		if (!GameManager.instance.gameOver) {

			if (beginning) {

				DoStartOfGameSpawning ();
				beginning = false;

			}

			if (Spawner.transform.position.y > startOfGameVerticalYPosition) {
		
				InfiniteModeSpawning ();

			}

		} else {

		}

	}

	void SetDifficultySettings() {

		switch (GameManager.instance.difficulty) {

		case 0:	// Beginner
			percentageChanceToSpawnPerNewPosition = 100f;
			percentageChanceToSpawnJump = 25f;
			percentageChanceToSpawnLeftRight = 25f;
			minimumJumpHeightValue = 1000f;
			maximumJumpHeightValue = 4000f;
			leftRightPlatformSpeed = 1f;
			GameManager.instance.ScoreMultiplier = 2.5f;
			platformSpawningInt = 2;
			break;

		default:
		case 1:	// Easy
			percentageChanceToSpawnPerNewPosition = 90f;
			percentageChanceToSpawnJump = 20f;
			percentageChanceToSpawnLeftRight = 20f;
			minimumJumpHeightValue = 1000f;
			maximumJumpHeightValue = 3000f;
			leftRightPlatformSpeed = Random.Range(1f, 3f);
			GameManager.instance.ScoreMultiplier = 5f;
			platformSpawningInt = 3;
			break;

		case 2:
			percentageChanceToSpawnPerNewPosition = 70f;
			percentageChanceToSpawnJump = 15f;
			percentageChanceToSpawnLeftRight = 25f;
			minimumJumpHeightValue = 1000f;
			maximumJumpHeightValue = 2500f;
			leftRightPlatformSpeed = Random.Range(2f, 4f);
			GameManager.instance.ScoreMultiplier = 10f;
			platformSpawningInt = 6;
			break;

		case 3:
			percentageChanceToSpawnPerNewPosition = 45f;
			percentageChanceToSpawnJump = 10f;
			percentageChanceToSpawnLeftRight = 30f;
			minimumJumpHeightValue = 750f;
			maximumJumpHeightValue = 2000f;
			leftRightPlatformSpeed = Random.Range(3f, 10f);
			GameManager.instance.ScoreMultiplier = 20f;
			platformSpawningInt = 8;
			break;

		case 4:
			percentageChanceToSpawnPerNewPosition = 15f;
			percentageChanceToSpawnJump = 5f;
			percentageChanceToSpawnLeftRight = 45f;
			minimumJumpHeightValue = 1100f;
			maximumJumpHeightValue = 1500f;
			leftRightPlatformSpeed = Random.Range(3f, 25f);
			GameManager.instance.ScoreMultiplier = 30f;
			platformSpawningInt = 10;
			break;
			
		}

	}

	void DoStartOfGameSpawning() {

		float verticalStartSpawnPosition = 5f, currentVerticalSpawnPosition;

		currentVerticalSpawnPosition = verticalStartSpawnPosition;

		while (currentVerticalSpawnPosition < nextValidSpawnLocation) {

			SpawnPlatform(currentVerticalSpawnPosition, 100f, 25f, 25f, true, false, false);

			currentVerticalSpawnPosition += minimumVerticalDistance;

		}

	}

	void InfiniteModeSpawning() {

		if (Spawner.transform.position.y > nextValidSpawnLocation) {

			SpawnPlatform(Spawner.transform.position.y, percentageChanceToSpawnPerNewPosition, percentageChanceToSpawnJump, percentageChanceToSpawnLeftRight, false, enableBrutalMode, enableRainbowMode);

			nextValidSpawnLocation = Spawner.transform.position.y + minimumVerticalDistance;

		}

	}

	void SpawnPlatform(float verticalPosition, float chanceToSpawnNewPosition = 100f, float chanceToSpawnJumpPlatform = 25f, float chanceToSpawnLeftRightPlatform = 25f, bool startSpawn = true, bool brutalMode = false, bool rainbowMode = false) {

		if (currentPlatform != null) {

			distanceBetweenPlatforms = Spawner.transform.position.y - currentPlatform.transform.position.y;

		}

		if (!startSpawn && !brutalMode && previousPlatform != null) {

			if (distanceBetweenPlatforms > maximumDistanceBetweenPlatforms) {

				forceSpawnPlatform = true;

				Debug.Log ("Distance between current and next platforms too much, so we're forcing a spawn. -- Previous: " + previousPlatform.transform.position + ", Current: " + currentPlatform.transform.position + ". distanceBetweenPlatforms: " + distanceBetweenPlatforms);

			}

		}

		float rand = Random.value;

		if (forceSpawnPlatform || (rand < (chanceToSpawnNewPosition / 100f))) {

			if (currentPlatform != null) {
				previousPlatform = currentPlatform;
			}
				
			if (forceSpawnPlatform) {

				verticalPosition = previousPlatform.transform.position.y + maximumDistanceBetweenPlatforms;

			}

			currentPlatform = Instantiate (platformsToSpawn [Random.Range (0, platformSpawningInt)],
											new Vector3 (Random.Range (-7.5f, 7.5f), verticalPosition),
											Quaternion.identity) as GameObject;
			
			currentPlatform.transform.SetParent (GeometryParent.transform);

			forceSpawnPlatform = false;

			if (rainbowMode) {

				currentPlatform.AddComponent<RainbowMode> ();

			}

			if (Random.value < (chanceToSpawnJumpPlatform / 100)) {

				currentPlatform.AddComponent<JumpPlatform2D> ().jumpHeight = Random.Range (minimumJumpHeightValue, maximumJumpHeightValue);

			}

			if (Random.value < (chanceToSpawnLeftRightPlatform / 100)) {

				LeftRight2D currentPlatformLR2D;
				currentPlatformLR2D = currentPlatform.AddComponent<LeftRight2D> ();

				currentPlatformLR2D.platformSpeed = leftRightPlatformSpeed;

				if (minimumHorizontalValue > maximumHorizontalValue) {

					float temp = maximumHorizontalValue;

					minimumHorizontalValue = maximumHorizontalValue;
					maximumHorizontalValue = temp;

				}

				currentPlatformLR2D.startingDirection = (Random.value < 0.5f) ? StartingDirection.LEFT : StartingDirection.RIGHT;

				currentPlatformLR2D.minimum = minimumHorizontalValue;
				currentPlatformLR2D.maximum = maximumHorizontalValue;

			}

		}

	}

}