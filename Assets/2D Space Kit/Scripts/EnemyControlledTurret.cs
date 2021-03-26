using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControlledTurret : MonoBehaviour
{
	public Transform player;
	public float cooldown;
	public float shootRange;
	private bool endShoot = true;
	public GameObject weaponPrefab;
	public GameObject[] barrelHardpoints;
	public float turretRotationSpeed = 3f;
	public float shootSpeed;
	private int barrelIndex = 0;


	private void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
	{

		//This makes the turret aim at the player
		Vector3 turretPosition = transform.position;
		Vector3 direction = new Vector3(player.position.x - turretPosition.x, player.position.y - turretPosition.y, player.position.z - turretPosition.z);
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.LerpAngle(transform.rotation.eulerAngles.z, (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f, turretRotationSpeed * Time.deltaTime)));


		if (barrelHardpoints != null && endShoot)
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
		GameObject bullet = (GameObject)Instantiate(weaponPrefab, barrelHardpoints[barrelIndex].transform.position, transform.rotation);

		bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * shootSpeed);
		bullet.GetComponent<Projectile>().firing_ship = transform.parent.gameObject;
		barrelIndex++; //This will cycle sequentially through the barrels in the barrel_hardpoints array

		if (barrelIndex >= barrelHardpoints.Length)
			barrelIndex = 0;
		endShoot = true;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, shootRange);
	}
}
