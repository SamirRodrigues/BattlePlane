using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public GameObject shootEffect;
	public GameObject hitEffect;
	public GameObject firingShip;
	
	// Use this for initialization
	void Start () {
		GameObject obj = (GameObject) Instantiate(shootEffect, transform.position  - new Vector3(0,0,5), Quaternion.identity); //Spawn muzzle flash
		obj.transform.parent = firingShip.transform;
		Destroy(this.gameObject, 5f); //Bullet will despawn after 5 seconds
	}
	
	void OnTriggerEnter2D(Collider2D col) {

		//Don't want to collide with the ship that's shooting this thing, nor another projectile.
		if (col.gameObject != firingShip && !col.gameObject.CompareTag("Projectile") && !col.gameObject.CompareTag("GlobalLimit") && !col.gameObject.CompareTag("Utility")) {
			
			if(col.CompareTag("Player"))
            {
				col.GetComponent<Player>().Damage(10);
            }
			Instantiate(hitEffect, transform.position, Quaternion.identity);
			Destroy(this.gameObject);
		}
	}
	
}
