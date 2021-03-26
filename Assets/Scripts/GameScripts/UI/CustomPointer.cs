using UnityEngine;
using System;
using System.Collections;

[System.Serializable]
public class CustomPointer : MonoBehaviour {
	
	public Texture pointerTexture;					//The image for the pointer, generally a crosshair or dot.	
	public float mouse_sensitivity_modifier = 15f;	//Speed multiplier for the mouse.	
	public static Vector2 pointerPosition;          //Position of the pointer in screen coordinates.

	private bool isAtive = true;
		
	void Start () 
	{
		Cursor.lockState = CursorLockMode.Confined; //	Lock the cursor inside the screen
		Cursor.visible = false;						//	Made the cursor invisible
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(isAtive)
        {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;


			float x_axis = Input.GetAxis("Mouse X");
			float y_axis = Input.GetAxis("Mouse Y");

			//Add the input to the pointer's position
			pointerPosition += new Vector2(x_axis * mouse_sensitivity_modifier,
											y_axis * mouse_sensitivity_modifier);


			//Keep the pointer within the bounds of the screen.
			pointerPosition.x = Mathf.Clamp(pointerPosition.x, 0, Screen.width);
			pointerPosition.y = Mathf.Clamp(pointerPosition.y, 0, Screen.height);
		}		
	}
	
	void OnGUI()
	{
		//Draw the pointer texture.
		if (pointerTexture != null)
			GUI.DrawTexture(new Rect(pointerPosition.x - (pointerTexture.width / 2), Screen.height - pointerPosition.y - (pointerTexture.height / 2), pointerTexture.width, pointerTexture.height), pointerTexture);
	}

	public void SetAtive(bool value)
    {
		isAtive = value;
    }
}
