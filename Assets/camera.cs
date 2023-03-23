using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{

	public float mouseSens = 100f;

	public Transform Controller;

	float xRotation = 0f;

	// Start is called before the first frame update
	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	// Update is called once per frame
	void Update()
	{
		float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime; // input manager in Unity GUI
		float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime; // mouseSens - чувствительность мыши
																			 // Time.deltaTime - поправка на производительность

		xRotation -= mouseY; // 
		xRotation = Mathf.Clamp(xRotation, -90f, 90f); // ограничение поворота
		transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // крутим камеру на угол поворота xRotation
		Controller.Rotate(Vector3.up * mouseX); // крутим персонажа, Vector3.up - вертикальный вектор 
	}
}
