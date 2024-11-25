using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display : Interactable
{
    [SerializeField] private string[] dialogue;
    [SerializeField] private Sprite background;

    private int interactIndex = 0;

    private UIManager uiManager;

    private void Awake()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    public override void Interact()
    {
        interactIndex = 0;
        uiManager.DisplayText(dialogue, background);
    }
}
