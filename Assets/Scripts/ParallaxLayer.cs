using UnityEngine;
using System.Collections;

public class ParallaxLayer : MonoBehaviour {
	private bool move;
	private Vector3 wantedPosition;
	private float speed = 2.5f;
	public float movement_resistance = 1f; //1 = no movement, 0.9 = some movement, 0.5 = more movement, etc, 0 = centered at origin, layer is now foreground
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		wantedPosition = Camera.main.transform.position * movement_resistance;
		wantedPosition.z = transform.position.z;
		

		if (move) { transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * speed);  }

		if (Vector3.Distance(transform.position, wantedPosition) < 0.02 && move)
        {
			move = false;
        }
		else
        {
			move = true;
        }
	}
}
