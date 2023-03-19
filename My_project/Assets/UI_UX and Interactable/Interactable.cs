using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent InteractAction;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    void FixedUpdate() {
        if (isInRange) {
            if (Input.GetKeyDown(interactKey)) {
                InteractAction.Invoke();
            }
        }
    }
}
