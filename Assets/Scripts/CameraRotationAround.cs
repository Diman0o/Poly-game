using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationAround : MonoBehaviour
{
	[SerializeField]
	private GameObject _panelGame;
	[SerializeField]
	private GameObject _panelEndGame;

	static public bool isLevelPassed;
	static public bool isMoveAnimationPlayed;
	static public bool isRotateAnimationPlayed;
	static public float animationSpeed;
	static public Vector3 currentEulerAngles;
	public Vector3 center;
	public Vector3 offset;
	public float sensitivity = 3;
	public static float zoom = -80;

	void Start()
	{
		isLevelPassed = false;
		isMoveAnimationPlayed = false;
		isRotateAnimationPlayed = false;
		animationSpeed = 0.01f;


		_panelGame.SetActive(PanelsManager.panelGameIsActive);
		_panelEndGame.SetActive(PanelsManager.panelEndGameIsActive);

		currentEulerAngles = LevelLoader.startRotation;
		center = new Vector3(0, 0, 0);
		offset = new Vector3(offset.x, offset.y, zoom);
		transform.position = center + offset;
		moveCamera();

	}

	void Update()
	{
		float distanceToWin = 1f;
		if (!isLevelPassed && Input.GetKey(KeyCode.Mouse0))
		{
			currentEulerAngles.x = currentEulerAngles.x + Input.GetAxis("Mouse X") * sensitivity;
			currentEulerAngles.y = currentEulerAngles.y + Input.GetAxis("Mouse Y") * sensitivity;
			moveCamera();
			distanceToWin = calcSquareDistanceToWin();
		}


		if (!isLevelPassed && distanceToWin < 1) //0.1
        {
			if (!string.IsNullOrWhiteSpace(DataStorer.accountId))
            {
				string[][] queryParams = new string[][] { new string[] { "accountId", DataStorer.accountId }, new string[] { "levelName", LevelsButtonsManager.levelNumber} };
				StartCoroutine(WebRequest.ProcessRequest("pass_level", queryParams, DataStorer.processSaveLevelAsPastOnServer));
			}
			string levelId;
			if (LevelsButtonsManager.levelMode == "main")
			{
				levelId = LevelsButtonsManager.levelNumber;
			}
			else
            {
				levelId = LevelsButtonsManager.lastCustomLevelId;
            }
			DataStorer.saveLevelAsPast(levelId);
			isLevelPassed = true;
			PanelsManager.panelGameIsActive = false;
			_panelGame.SetActive(PanelsManager.panelGameIsActive);
			PanelsManager.panelEndGameIsActive = true;
			_panelEndGame.SetActive(PanelsManager.panelEndGameIsActive);
		}

		playWinAnimationOnLevelPassed();
	}

	void moveCamera()
    {
		transform.localEulerAngles = new Vector3(-currentEulerAngles.y, currentEulerAngles.x, 0);
		transform.position = transform.localRotation * offset + center;
	}

	float calcSquareDistanceToWin()
	{
		return calcDistance(offset, transform.position);
	}

	static float calcDistance(Vector3 a, Vector3 b)
    {
		return
			Mathf.Pow(a.x - b.x, 2) +
			Mathf.Pow(a.y - b.y, 2) +
			Mathf.Pow(a.z - b.z, 2);
	}

	void playWinAnimationOnLevelPassed()
    {
		if (isLevelPassed) {
			playMoveAnimation();
			playRotateAnimation();
		}
	}

	void playMoveAnimation()
    {
		float animationSpeed = 0.07f;
		if (!isMoveAnimationPlayed)
		{
			Vector3 direction = (offset - transform.position);
			Vector3 directionPoint = transform.position + direction * animationSpeed;
			float r = Mathf.Abs(zoom);
			float t = r / Mathf.Sqrt(Mathf.Pow(directionPoint.x, 2) + Mathf.Pow(directionPoint.y, 2) + Mathf.Pow(directionPoint.z, 2)); //For optimization reazons possible to change sqrt that using fast inverse square root https://en.wikipedia.org/wiki/Fast_inverse_square_root 
			Vector3 pointOnSphereLocatedInDirection = directionPoint * t;
			transform.position = pointOnSphereLocatedInDirection;
			if (calcDistance(transform.position, offset) < 0.001)
			{
				transform.position = offset;
				isMoveAnimationPlayed = true;
			}
		}
	}

	void playRotateAnimation()
    {
		float animationSpeed = 0.1f;
		if (!isRotateAnimationPlayed)
		{
			Vector3 worldUp = Vector3.up.normalized;
			Vector3 cameraUp = transform.up.normalized;
			float cameraDiff =
				Mathf.Pow(worldUp.x - cameraUp.x, 2) +
				Mathf.Pow(worldUp.y - cameraUp.y, 2) +
				Mathf.Pow(worldUp.z - cameraUp.z, 2);
			Vector3 newCameraUp = cameraUp + (worldUp - cameraUp) * animationSpeed;
			transform.LookAt(center, newCameraUp);
			if (cameraDiff < 0.001)
			{
				transform.LookAt(center, Vector3.up);
				isRotateAnimationPlayed = true;
			}
		}
	}
}

