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
        textComponent.text = string.Empty;
        startDialogue();
    }

    void FixedUpdate() {
        if (Input.GetMouseButtonDown(0))
            nextLine();
        else {
            StopAllCoroutines();
            textComponent.text = lines[index];
        }
    }

    void startDialogue() {
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
