using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {
	public RectTransform healthBar;
	public bool destroyOnDeath = false;

	public const int maxHealth = 100;
	[SyncVar(hook = "OnChangeHealth")]
	public int currentHealth = maxHealth;

	private NetworkStartPosition[] spawnPositions;

	void Start() {
		if (isServer) {
			OnChangeHealth(currentHealth);
		}
		
		if (isLocalPlayer) {
			spawnPositions = FindObjectsOfType<NetworkStartPosition>();
		}
	}

	public void TakeDamage(int amount) {

		if (!isServer) {
			return;
		}

		currentHealth -= amount;
		if (currentHealth < 0) {
			Debug.Log("Dead!");

			if (destroyOnDeath) {
				Destroy(gameObject);
			} else {
				currentHealth = maxHealth;
				RpcRespawn();
			}
		}
	}

	[ClientRpc]
	void RpcRespawn() {
		if (isLocalPlayer) {
			if (spawnPositions != null && spawnPositions.Length > 0) {
				transform.position = spawnPositions[Random.Range(0, spawnPositions.Length)].transform.position;
			} else {
				transform.position = Vector3.zero;
			}
		}
	}

	void OnChangeHealth (int health) {
		currentHealth = health;
		healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
	}
}
