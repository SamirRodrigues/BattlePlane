using UnityEngine;

public class PSDestroy : MonoBehaviour {

	// Particle System Destroy
	void Start () {
		Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);
	}
	
}
