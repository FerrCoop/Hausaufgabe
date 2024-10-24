using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public Item CurrentItem { get; private set; }

    [SerializeField] private float speed;
    [SerializeField] private Transform itemSpot;

    private Interactable targetInteractable;
    private Vector3 targetPos;

    private const string HELD_ITEM_LAYER_NAME = "Held Items";

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        targetPos = transform.position;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Update()
    {
        if (transform.position != targetPos)
        {
            float _distance = Vector3.Distance(transform.position, targetPos);
            transform.position = Vector3.Lerp(transform.position, targetPos, Mathf.Clamp(speed * Time.deltaTime / _distance, 0f, 1f));
        } 
        else if (targetInteractable != null)
        {
            targetInteractable.Interact();
            targetInteractable = null;
        }
    }

    public void MoveTo(Interactable _interactable)
    {
        targetInteractable = _interactable;
        targetPos = _interactable.interactSpot.position;
    }

    public void MoveTo(Vector3 _pos)
    {
        targetInteractable = null;
        //drop item
        targetPos = _pos;
    }

    public void SetCurrentItem(Item _item)
    {  
        if (_item == null)
        {
            //drop current item
            //update animation
        }
        else
        {
            if (CurrentItem == null)
            {
                _item.transform.SetParent(itemSpot);
                _item.transform.position = itemSpot.transform.position;
                _item.transform.rotation = itemSpot.transform.rotation;
                _item.GetComponent<SpriteRenderer>().sortingLayerName = HELD_ITEM_LAYER_NAME;
            }
        }
        CurrentItem = _item;
    }
}
