using System.Collections;
using UnityEngine.Networking;
using UnityEngine;

public class Bullet : NetworkBehaviour {
	void OnCollisionEnter(Collision collision) {
		Health health = collision.gameObject.GetComponent<Health>();
		if (health != null) {
			health.TakeDamage(10);
		}

		Destroy(gameObject);
	}
}
