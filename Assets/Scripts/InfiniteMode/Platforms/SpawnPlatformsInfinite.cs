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

	float nextValidSpawnLocation, distanceBetweenPlatforms;
	bool forceSpawnPlatform = false;

	public GameObject debug_information;

	void Start() {

		enableBrutalMode = GameManager.instance.infiniteMode_Brutal;
		enableRainbowMode = GameManager.instance.infiniteMode_Rainbow;

		nextValidSpawnLocation = startOfGameVerticalYPosition;

		DoStartOfGameSpawning ();

	}

	void DoStartOfGameSpawning() {

		float verticalStartSpawnPosition = 5f, currentVerticalSpawnPosition;

		currentVerticalSpawnPosition = verticalStartSpawnPosition;

		while (currentVerticalSpawnPosition < nextValidSpawnLocation) {

			SpawnPlatform(currentVerticalSpawnPosition, 95f, 10f, 25f, true, enableBrutalMode, enableRainbowMode);

			currentVerticalSpawnPosition += minimumVerticalDistance;

		}

	}

	void Update() {

		if (!GameManager.instance.gameOver) {

			if (Spawner.transform.position.y > startOfGameVerticalYPosition) {
		
				// ChangeSpawningSettingsOnSliderChange ();
				InfiniteModeSpawning ();

			}

		} else {

			Debug.Log ("GameManager.instance.gameOver");
			debug_information = GameObject.FindGameObjectWithTag ("DEBUG_INFORMATION");

			if (Social.localUser.authenticated) {

				debug_information.GetComponent<Text>().text = "LOGGED IN -- Score: " + GameManager.instance.GetScore ().ToString ();

					Social.ReportScore (long.Parse (GameManager.instance.GetScore ().ToString ()), GameManager.instance.Leaderboard_InfiniteMode,
					
						(bool success) => {
						
							if (success) {


							} else {

								

							}

					});

			} else {

				debug_information.GetComponent<Text>().text = "NOT LOGGED IN -- Score: " + GameManager.instance.GetScore ().ToString ();

			}

		}

	}

	void InfiniteModeSpawning() {

		if (Spawner.transform.position.y > nextValidSpawnLocation) {

			SpawnPlatform(Spawner.transform.position.y, percentageChanceToSpawnPerNewPosition, percentageChanceToSpawnJump, percentageChanceToSpawnLeftRight, false, enableBrutalMode, enableRainbowMode);

			nextValidSpawnLocation = Spawner.transform.position.y + minimumVerticalDistance;

		}

	}

	void SpawnPlatform(float verticalPosition, float chanceToSpawnNewPosition = 75f, float chanceToSpawnJumpPlatform = 25f, float chanceToSpawnLeftRightPlatform = 50f, bool starting = true, bool brutalMode = false, bool rainbowMode = false) {

		if (currentPlatform != null) {

			distanceBetweenPlatforms = Spawner.transform.position.y - currentPlatform.transform.position.y;

		}

		if (!starting && !brutalMode && previousPlatform != null) {

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

//	void ChangeSpawningSettingsOnSliderChange() {
//
//		float sliderValue = DEBUG_DifficultySlider.value;
//
//		percentageChanceToSpawnPerNewPosition 	= Mathf.Clamp(sliderValue, 20f, 100f);
//		percentageChanceToSpawnJump 			= Mathf.Clamp(sliderValue / 6f, 10f, 100f);
//		percentageChanceToSpawnLeftRight 		= Mathf.Clamp(sliderValue / 3f, 5f, 100f);
//
//	}

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

//			if (!brutalModeEnabled && previousPlatform != null && distanceBetweenPlatforms > maximumDistanceBetweenPlatforms) {
//
//				if (previousPlatform.GetComponent<JumpPlatform2D>() != null) {
//
//					Destroy (previousPlatform.GetComponent<JumpPlatform2D> ());
//
//				}
//
//				JumpPlatform2D previousPlatformJump = previousPlatform.AddComponent<JumpPlatform2D> ();
//
//				previousPlatformJump.jumpHeight = distanceBetweenPlatforms * 100f;
//
//			}