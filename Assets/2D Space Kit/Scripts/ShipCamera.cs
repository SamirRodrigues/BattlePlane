using UnityEngine;
using System.Collections;

public class ShipCamera : MonoBehaviour {

	public Transform target_object;
	public float follow_tightness;
	Vector3 wanted_position;

	public float limitPositiveX = 150;
	public float limitNegativeX = -150;
	public float limitPositiveY = 200;
	public float limitNegativeY = -200;
	

	// Use this for initialization
	void Start () {
	
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if(transform.position.x <= limitPositiveX &&
			transform.position.x >= limitNegativeX &&
			transform.position.y <= limitPositiveY &&
			transform.position.y >= limitNegativeY)
        {
			wanted_position = target_object.position;
			wanted_position.z = transform.position.z;
			transform.position = Vector3.Lerp(transform.position, wanted_position, Time.deltaTime * follow_tightness);
		}
		
		
	}	
}
