using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Positioner : MonoBehaviour
{
    private SpriteRenderer myRenderer;

    private void Awake()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        myRenderer.sortingOrder = -Mathf.RoundToInt(transform.position.y * 100) + Mathf.RoundToInt(-transform.position.x);
    }
}
