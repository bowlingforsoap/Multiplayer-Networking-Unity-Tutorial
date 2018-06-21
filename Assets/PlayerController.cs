using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {
	
	// Update is called once per frame
	void Update () {

		if (!isLocalPlayer) {
			return;
		}

		float x = Input.GetAxis("Horizontal") * Time.deltaTime * 150f;
		float z = Input.GetAxis("Vertical") * Time.deltaTime * 3f;

		transform.Rotate(0f, x, 0f);
		transform.Translate(0f, 0f, z);
	}

	public override void OnStartLocalPlayer() {
		GetComponent<MeshRenderer>().material.color = Color.magenta;
	}
}
