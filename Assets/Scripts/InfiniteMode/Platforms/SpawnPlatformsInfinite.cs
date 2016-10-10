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
	public float minimumVerticalDistance = 2f, percentageChanceToSpawnPerNewPosition = 50f, maximumDistanceBetweenPlatforms = 10f;

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

	float nextValidSpawnLocation, distanceBetweenPlatforms;
	bool forceSpawnPlatform = false;

	void Start() {

		enableBrutalMode = GameManager.instance.infiniteMode_Brutal;
		enableRainbowMode = GameManager.instance.infiniteMode_Rainbow;

		nextValidSpawnLocation = startOfGameVerticalYPosition;

		DoStartOfGameSpawning ();

		SetDifficultySettings ();

	}

	void Update() {

		if (!GameManager.instance.gameOver) {

			if (Spawner.transform.position.y > startOfGameVerticalYPosition) {
		
				// ChangeSpawningSettingsOnSliderChange ();
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
			break;

		case 2:
			percentageChanceToSpawnPerNewPosition = 70f;
			percentageChanceToSpawnJump = 15f;
			percentageChanceToSpawnLeftRight = 25f;
			minimumJumpHeightValue = 1000f;
			maximumJumpHeightValue = 2500f;
			leftRightPlatformSpeed = Random.Range(2f, 4f);
			GameManager.instance.ScoreMultiplier = 10f;
			break;

		case 3:
			percentageChanceToSpawnPerNewPosition = 45f;
			percentageChanceToSpawnJump = 10f;
			percentageChanceToSpawnLeftRight = 30f;
			minimumJumpHeightValue = 750f;
			maximumJumpHeightValue = 2000f;
			leftRightPlatformSpeed = Random.Range(3f, 10f);
			GameManager.instance.ScoreMultiplier = 20f;
			break;

		case 4:
			percentageChanceToSpawnPerNewPosition = 15f;
			percentageChanceToSpawnJump = 5f;
			percentageChanceToSpawnLeftRight = 45f;
			minimumJumpHeightValue = 750f;
			maximumJumpHeightValue = 1250f;
			leftRightPlatformSpeed = Random.Range(3f, 25f);
			GameManager.instance.ScoreMultiplier = 30f;
			break;
			
		}

	}

	void DoStartOfGameSpawning() {

		float verticalStartSpawnPosition = 5f, currentVerticalSpawnPosition;

		currentVerticalSpawnPosition = verticalStartSpawnPosition;

		while (currentVerticalSpawnPosition < nextValidSpawnLocation) {

			SpawnPlatform(currentVerticalSpawnPosition, 95f, 10f, 25f, enableBrutalMode, enableRainbowMode);
			//SpawnPlatform(currentVerticalSpawnPosition, percentageChanceToSpawnPerNewPosition, percentageChanceToSpawnJump, percentageChanceToSpawnLeftRight, false, enableRainbowMode);

			currentVerticalSpawnPosition += minimumVerticalDistance;

		}

	}

	void InfiniteModeSpawning() {

		if (Spawner.transform.position.y > nextValidSpawnLocation) {

			SpawnPlatform(Spawner.transform.position.y, percentageChanceToSpawnPerNewPosition, percentageChanceToSpawnJump, percentageChanceToSpawnLeftRight, enableBrutalMode, enableRainbowMode);

			nextValidSpawnLocation = Spawner.transform.position.y + minimumVerticalDistance;

		}

	}

	void SpawnPlatform(float verticalPosition, float chanceToSpawnNewPosition = 75f, float chanceToSpawnJumpPlatform = 25f, float chanceToSpawnLeftRightPlatform = 50f, bool brutalMode = false, bool rainbowMode = false) {

		if (currentPlatform != null) {

			distanceBetweenPlatforms = Spawner.transform.position.y - currentPlatform.transform.position.y;

		}

		if (!brutalMode && previousPlatform != null) {

			if (distanceBetweenPlatforms > maximumDistanceBetweenPlatforms) {

				forceSpawnPlatform = true;

				Debug.Log ("Distance between current and next platforms too much, so we're forcing a spawn. -- Previous: " + previousPlatform.transform.position + ", Current: " + currentPlatform.transform.position + ". distanceBetweenPlatforms: " + distanceBetweenPlatforms);

			}

		}

		if (forceSpawnPlatform || (Random.value < (percentageChanceToSpawnPerNewPosition / 100))) {

			if (currentPlatform != null) {
				previousPlatform = currentPlatform;
			}
				
			if (forceSpawnPlatform) {

				verticalPosition = previousPlatform.transform.position.y + maximumDistanceBetweenPlatforms;

			}

			currentPlatform = Instantiate (platformsToSpawn [Random.Range (0, platformsToSpawn.Length)],
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