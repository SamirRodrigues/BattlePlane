using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControlledTurret : MonoBehaviour
{
	public Transform player;
	public float cooldown;
	public float shootRange;
	private bool endShoot = true;
	public GameObject weapon_prefab;
	public GameObject[] barrel_hardpoints;
	public float turret_rotation_speed = 3f;
	public float shot_speed;
	int barrel_index = 0;


	private void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
	{

		//This makes the turret aim at the mouse position (Controlled by CustomPointer, but you can replace CustomPointer.pointerPosition with Input.MousePosition and it should work)
		Vector3 turretPosition = transform.position;
		Vector3 direction = new Vector3(player.position.x - turretPosition.x, player.position.y - turretPosition.y, player.position.z - turretPosition.z);
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.LerpAngle(transform.rotation.eulerAngles.z, (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f, turret_rotation_speed * Time.deltaTime)));


		if (barrel_hardpoints != null && endShoot)
		{
			if(Vector2.Distance(player.position, transform.position) <= shootRange)
            {
				endShoot = false;
				StartCoroutine(NextShoot(cooldown));
            }
		}

	}

	IEnumerator NextShoot(float time)
    {
		yield return new WaitForSeconds(time);
		GameObject bullet = (GameObject)Instantiate(weapon_prefab, barrel_hardpoints[barrel_index].transform.position, transform.rotation);

		bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * shot_speed);
		bullet.GetComponent<Projectile>().firing_ship = transform.parent.gameObject;
		barrel_index++; //This will cycle sequentially through the barrels in the barrel_hardpoints array

		if (barrel_index >= barrel_hardpoints.Length)
			barrel_index = 0;
		endShoot = true;
	}
}
