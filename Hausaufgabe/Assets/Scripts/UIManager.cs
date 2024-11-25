using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject displayParent;
    [SerializeField] private TextMeshProUGUI textObject;
    [SerializeField] private Image displayBG;

    private string[] currentDialogue;
    private int interactIndex;    

    public void DisplayText(string[] _dialogue, Sprite _bg)
    {
        currentDialogue = _dialogue;
        interactIndex = 0;
        displayParent.SetActive(true);
        displayBG.sprite = _bg;
        StartCoroutine(AnimateText(currentDialogue[interactIndex]));
    }

    public void IncrementDisplay(int _change)
    {
        interactIndex = Mathf.Clamp(interactIndex + _change, 0, currentDialogue.Length);
        if (interactIndex >= currentDialogue.Length)
        {
            displayParent.SetActive(false);
        }
        else
        {
            StartCoroutine(AnimateText(currentDialogue[interactIndex]));
        }
    }

    private IEnumerator AnimateText(string _text)
    {
        textObject.text = "";
        for (int i = 0; i < _text.Length; i++)
        {
            textObject.text += _text[i];
            yield return new WaitForSeconds(0.05f);
        }
    }
}
