using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public GameObject[] spawnPoints;

    public void spawnPlayer(GameObject player) {
        GameObject spawnPoint = respawnOnNearest();
        player.transform.position = spawnPoint.transform.position;
        Debug.Log("Player has respawned");
    }

    GameObject respawnOnNearest() {
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }
}
