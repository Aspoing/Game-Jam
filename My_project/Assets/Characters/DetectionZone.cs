using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public string tagTarget = "Player";
    public List<Collider2D> detectObjs = new List<Collider2D>();
    public Collider2D collider;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == tagTarget) {
        Debug.Log("Collider  player found");
           detectObjs.Add(other);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == tagTarget) {
           detectObjs.Remove(other);
        }
    }
}
