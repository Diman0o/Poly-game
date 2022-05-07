using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationAround : MonoBehaviour
{
	static public Vector3 currentEulerAngles;
	public Vector3 center;
	public Vector3 offset;
	public float sensitivity = 3;
	public static float zoom = -10;

	void Start()
	{
		currentEulerAngles = LevelLoader.startRotation;
		center = new Vector3(0, 0, 0);
		offset = new Vector3(offset.x, offset.y, zoom);
		transform.position = center + offset;
		Update();
	}

	void Update()
	{
        if (Input.GetKey(KeyCode.Mouse0))
        {
			currentEulerAngles.x = currentEulerAngles.x + Input.GetAxis("Mouse X") * sensitivity;
			currentEulerAngles.y = currentEulerAngles.y + Input.GetAxis("Mouse Y") * sensitivity;

		}
		transform.localEulerAngles = new Vector3(-currentEulerAngles.y, currentEulerAngles.x, 0);
		transform.position = transform.localRotation * offset + center;
	}

}

