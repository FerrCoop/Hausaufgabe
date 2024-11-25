using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public Transform interactSpot;
    [SerializeField] private SpriteRenderer highlightRenderer;

    private SpriteRenderer myRenderer;

    private void Awake()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        myRenderer.sortingOrder = -Mathf.RoundToInt(transform.position.y * 100) + Mathf.RoundToInt(-transform.position.x);
        highlightRenderer.enabled = false;
        highlightRenderer.sortingOrder = myRenderer.sortingOrder + 1;
    }

    public void ToggleHighlight()
    {
        highlightRenderer.enabled = !highlightRenderer.enabled;
    }

    public abstract void Interact();
}
