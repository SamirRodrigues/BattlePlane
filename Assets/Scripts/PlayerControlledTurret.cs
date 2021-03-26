using UnityEngine;
using System.Collections;

public class PlayerControlledTurret : MonoBehaviour {

	public GameObject weaponPrefab;
	public GameObject[] barrelHardpoints;
	public float turretRotationSpeed = 3f;
	public float shootSpeed;
	int barrelIndex = 0;

	
	void Start () {
	
	}


	void Update () {
	
		//This makes the turret aim at the mouse position (Controlled by CustomPointer, but you can replace CustomPointer.pointerPosition with Input.MousePosition and it should work)
		Vector2 turretPosition = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 direction = CustomPointer.pointerPosition - turretPosition;
		transform.rotation = Quaternion.Euler (new Vector3(0, 0, Mathf.LerpAngle(transform.rotation.eulerAngles.z, (Mathf.Atan2 (direction.y,direction.x) * Mathf.Rad2Deg) - 90f, turretRotationSpeed * Time.deltaTime)));


		if (Input.GetMouseButtonDown(0) && barrelHardpoints != null) 
		{
			GameObject bullet = (GameObject) Instantiate(weaponPrefab, barrelHardpoints[barrelIndex].transform.position, transform.rotation);

			bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * shootSpeed);
			bullet.GetComponent<Projectile>().firingShip = transform.parent.gameObject;
			barrelIndex++; //This will cycle sequentially through the barrels in the barrel_hardpoints array
			
			if (barrelIndex >= barrelHardpoints.Length)
				barrelIndex = 0;			
		}
	
	}
}
