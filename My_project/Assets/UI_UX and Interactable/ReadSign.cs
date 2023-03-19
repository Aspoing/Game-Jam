using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadSign : MonoBehaviour
{
    public bool isRead;
    public Dialogue dialogue;

    public string[] lines;
    
    public void readSign() {
        Debug.Log("i'm being read");
        dialogue.setNewText(lines);
        dialogue.startDialogue();
    } 
}