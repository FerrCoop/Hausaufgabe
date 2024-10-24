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
        highlightRenderer.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100) + Mathf.RoundToInt(-transform.position.x) + 1;
    }

    private void OnMouseEnter()
    {
        highlightRenderer.enabled = true;
    }

    private void OnMouseExit()
    {
        highlightRenderer.enabled = false;
    }

    private void OnMouseDown()
    {
        Debug.Log("Clicked");
        Player.Instance.MoveTo(this);
    }

    public abstract void Interact();
}
