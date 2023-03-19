using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public GameObject[] spawnPoints;

    public void spawnPlayer(GameObject player) {
        GameObject spawnPoint = respawnOnNearest();
        player.transform.position = spawnPoint.transform.position;
        DamageableCharacter damageableCharacter = player.GetComponent<DamageableCharacter>();
        damageableCharacter.healthBar.setHealth(10);
        damageableCharacter.Health = 9;
    }

    GameObject respawnOnNearest() {
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }
}
