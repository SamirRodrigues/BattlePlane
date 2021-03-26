using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float maxVelocity = 3;
	public float acceleration_amount = 1f;
	public float rotation_speed = 1f;
	public GameObject turret;
	public float turret_rotation_speed = 3f;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
	{
		
		Moviment();

	}

	public static Player Instance { get; private set;  }

	void Moviment()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}

		if (Input.GetKey(KeyCode.W)) 
		{

			if (GetComponent<Rigidbody2D>().velocity.x <= maxVelocity && GetComponent<Rigidbody2D>().velocity.x >= (-1 * maxVelocity) &&    // Limit X velocity
				GetComponent<Rigidbody2D>().velocity.y <= maxVelocity && GetComponent<Rigidbody2D>().velocity.y >= (-1 * maxVelocity))		// Limit y velocuty
			{
				GetComponent<Rigidbody2D>().AddForce(transform.up * acceleration_amount * Time.deltaTime);
            }
			
		}

		if (Input.GetKey(KeyCode.S)) {
			GetComponent<Rigidbody2D>().AddForce((-transform.up) * acceleration_amount * Time.deltaTime);			
		}
		
		// Rotate
		if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.LeftShift)) {
			GetComponent<Rigidbody2D>().AddTorque(-rotation_speed  * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftShift)) {
			GetComponent<Rigidbody2D>().AddTorque(rotation_speed  * Time.deltaTime);			
		}

		// Strafeing
		if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
		{
			GetComponent<Rigidbody2D>().AddForce((-transform.right) * acceleration_amount * 0.6f * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
		{
			GetComponent<Rigidbody2D>().AddForce((transform.right) * acceleration_amount * 0.6f * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.Space)) {
			GetComponent<Rigidbody2D>().angularVelocity = Mathf.Lerp(GetComponent<Rigidbody2D>().angularVelocity, 0, rotation_speed * 0.06f * Time.deltaTime);
			GetComponent<Rigidbody2D>().velocity = Vector2.Lerp(GetComponent<Rigidbody2D>().velocity, Vector2.zero, acceleration_amount * 0.06f * Time.deltaTime);
		}	
		
		
		if (Input.GetKey(KeyCode.H)) {
			transform.position = new Vector3(0,0,0);
		}	
	}
    
}
