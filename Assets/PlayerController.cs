using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	public GameObject bulletPrefab;
	public Transform bulletSpawn;
	
	// Update is called once per frame
	void Update () {

		if (!isLocalPlayer) {
			return;
		}

		float x = Input.GetAxis("Horizontal") * Time.deltaTime * 150f;
		float z = Input.GetAxis("Vertical") * Time.deltaTime * 3f;

		transform.Rotate(0f, x, 0f);
		transform.Translate(0f, 0f, z);

		if (Input.GetKeyDown(KeyCode.Space)) {
			CmdFire();
		}
	}

	// This commnad code is called by client ..
	// .. but run by server!
	[Command]
	void CmdFire() {
		// Instantiate bullet from bulletPrefab
		GameObject bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = 6f * bullet.transform.forward;

		NetworkServer.Spawn(bullet);

		// Destroy bullet after 2 secons
		Destroy(bullet, 2.0f);
	}

	public override void OnStartLocalPlayer() {
		GetComponent<MeshRenderer>().material.color = Color.magenta;
	}
}
