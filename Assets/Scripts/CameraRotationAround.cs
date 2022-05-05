using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationAround : MonoBehaviour
{
	public Vector3 currentEulerAngles;
	public Vector3 pointOfView;
	public Vector3 offset;
	public float sensitivity = 3;
	public float zoom = 5;

	void Start()
	{
		currentEulerAngles = new Vector3(0, 0, 0);
		pointOfView = new Vector3(0, 0, 0);
		offset = new Vector3(offset.x, offset.y, -zoom / 2);
		transform.position = pointOfView + offset;
	}

	void Update()
	{
        if (Input.GetKey(KeyCode.Mouse0))
        {
			currentEulerAngles.x = currentEulerAngles.x + Input.GetAxis("Mouse X") * sensitivity;
			currentEulerAngles.y = currentEulerAngles.y + Input.GetAxis("Mouse Y") * sensitivity;
			transform.localEulerAngles = new Vector3(-currentEulerAngles.y, currentEulerAngles.x, 0);
			transform.position = transform.localRotation * offset + pointOfView;
		}
	}

}

