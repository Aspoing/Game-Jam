using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;

    // Start is called before the first frame update
    void Start() {
        gameObject.SetActive(false);
        textComponent.text = string.Empty;
        startDialogue();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            nextLine();
            Debug.Log("A is pressed");
        }
        else {
            StopAllCoroutines();
            textComponent.text = lines[index];
        }
    }

    public void setNewText(string[] newLines) {
        lines = newLines;
    }

    public void startDialogue() {
        gameObject.SetActive(true);
        index = 0;
        StartCoroutine(Typeline());
    }

    void nextLine() {
        if (index < lines.Length - 1) {
            ++index;
            textComponent.text = string.Empty;
            StartCoroutine(Typeline());
        } else
            gameObject.SetActive(false);
    }

    IEnumerator Typeline() {
        foreach (char c in lines[index].ToCharArray()) {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
