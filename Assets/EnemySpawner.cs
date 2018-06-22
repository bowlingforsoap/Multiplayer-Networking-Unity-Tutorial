using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour {

	public GameObject enemyPrefab;
	public int numberOfEnemies;

	public override void OnStartServer() {
		Vector3 spawnPosition;
		Quaternion spawnOrientation;

		for (int i = 0; i < numberOfEnemies; i++) {
			spawnPosition = new Vector3(Random.Range(-8f, 8f), 0f, Random.Range(-8f, 8f));
			spawnOrientation = Quaternion.Euler(0f, Random.Range(0,180), 0f);
			GameObject enemy = Instantiate(enemyPrefab, spawnPosition, spawnOrientation);
			NetworkServer.Spawn(enemy);
		}
	}
}
