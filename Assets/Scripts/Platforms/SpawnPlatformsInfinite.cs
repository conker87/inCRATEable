using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpawnPlatformsInfinite : MonoBehaviour {

	public GameObject[] platformsToSpawn;
	GameObject previousPlatform, currentPlatform;

	[Header("Spawning Settings")]
	public GameObject GeometryParent;
	public float minimumVerticalDistance = 2f, percentageChanceToSpawnPerNewPosition = 50f;

	[Header("Infinite Mode Settings")]
	public bool brutalMode = false;
	string brutalModeTooltip = "The game is set so that if there is a platform that has spawned too high for you to reach then the game will make in a jump platform so you can reach it, turning this on removes that code.";

	[Header("Jump Component Values")]
	public float percentageChanceToSpawnJump = 5f;
	public float minimumJumpHeightValue = 1000f;
	public float maximumJumpHeightValue = 2500f;

	[Header("Left/Right Component Values")]
	public float percentageChanceToSpawnLeftRight = 10f;
	public float minimumHorizontalValue = -5f;
	public float maximumHorizontalValue = 5f;

	[Header("Debug")]
	public Slider DEBUG_DifficultySlider;
	public Toggle DEBUG_BrutalModeToggle;

	float nextValidSpawnLocation, distanceBetweenPlatforms;

	float startOfGameVerticalYPosition = 30f;

	void Start() {

		nextValidSpawnLocation = startOfGameVerticalYPosition + minimumVerticalDistance;

		DoStartOfGameSpawning ();

	}

	void Update() {

		brutalMode = (DEBUG_BrutalModeToggle.isOn) ? true : false;

		if (transform.position.y > startOfGameVerticalYPosition) {

			ChangeSpawningSettingsOnSliderChange ();
			InfiniteModeSpawning ();

		}

	}

	void DoStartOfGameSpawning() {

		//Do shit.

	}

	void ChangeSpawningSettingsOnSliderChange() {

		float sliderValue = DEBUG_DifficultySlider.value;

		percentageChanceToSpawnPerNewPosition 	= Mathf.Clamp(100 - sliderValue, 1, 100);
		percentageChanceToSpawnJump 			= Mathf.Clamp(100 - sliderValue, 1, 100);
		percentageChanceToSpawnLeftRight 		= Mathf.Clamp(100 - sliderValue, 1, 100);

	}

	void InfiniteModeSpawning() {

		//Debug.Log ("Y: " + transform.position.y + " (" + nextValidSpawnLocation + ")");

		if (transform.position.y > nextValidSpawnLocation) {

			if (Random.value < (percentageChanceToSpawnPerNewPosition / 100)) {

				previousPlatform = (currentPlatform != null) ? currentPlatform : null;

				currentPlatform = Instantiate (platformsToSpawn [Random.Range (0, platformsToSpawn.Length)], new Vector3 (Random.Range (-7.5f, 7.5f), transform.position.y), Quaternion.identity) as GameObject;
				currentPlatform.transform.SetParent (GeometryParent.transform);

				if (Random.value < (percentageChanceToSpawnJump / 100)) {

					JumpPlatform2D currentPlatformJP2D;
					currentPlatformJP2D = currentPlatform.AddComponent<JumpPlatform2D> ();

					currentPlatformJP2D.jumpHeight = Random.Range (minimumJumpHeightValue, maximumJumpHeightValue);

				}

				if (Random.value < (percentageChanceToSpawnLeftRight / 100)) {

					LeftRight2D currentPlatformLR2D;
					currentPlatformLR2D = currentPlatform.AddComponent<LeftRight2D> ();

					if (minimumHorizontalValue > maximumHorizontalValue) {

						float temp = maximumHorizontalValue;

						minimumHorizontalValue = maximumHorizontalValue;
						maximumHorizontalValue = temp;

					}

					currentPlatformLR2D.startingDirection = (Random.value < 0.5f) ? StartingDirection.LEFT : StartingDirection.RIGHT;

					currentPlatformLR2D.minimum = minimumHorizontalValue;
					currentPlatformLR2D.maximum = maximumHorizontalValue;

				}

				if (previousPlatform != null) {

					distanceBetweenPlatforms = currentPlatform.transform.position.y - previousPlatform.transform.position.y;

				}

				if (!brutalMode && previousPlatform != null && distanceBetweenPlatforms > 10) {

					if (previousPlatform.GetComponent<JumpPlatform2D>() != null) {

						Destroy (previousPlatform.GetComponent<JumpPlatform2D> ());

					}

					JumpPlatform2D previousPlatformJump = previousPlatform.AddComponent<JumpPlatform2D> ();

					previousPlatformJump.jumpHeight = distanceBetweenPlatforms * 100f;

				}

			}

			nextValidSpawnLocation = transform.position.y + minimumVerticalDistance;

		}

	}

}
