using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public Item CurrentItem { get; private set; }

    [SerializeField] private float speed;
    [SerializeField] private Transform itemSpot;
    [Space]
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private LayerMask floorLayer;

    private Camera cam;
    private Animator animator;
    private UIManager uiManager;

    private Vector2 mousePrevious;
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
        cam = Camera.main;
        animator = GetComponent<Animator>();
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
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
        Physics2D.queriesStartInColliders = false;
        Vector2 _mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] _hits = Physics2D.RaycastAll(mousePrevious, (_mousePos - mousePrevious).normalized, Vector2.Distance(_mousePos, mousePrevious), interactableLayer);
        RaycastHit2D[] _hits2 = Physics2D.RaycastAll(_mousePos, (mousePrevious - _mousePos).normalized, Vector2.Distance(_mousePos, mousePrevious), interactableLayer);
        List<RaycastHit2D> _hits3 = new List<RaycastHit2D>();
        _hits3.AddRange(_hits);
        _hits3.AddRange(_hits2);
        foreach (RaycastHit2D _hit in _hits3)
        {
            if (_hit.collider.TryGetComponent(out Interactable _interactable))
            {
                _interactable.ToggleHighlight();
            }
        }

        mousePrevious = _mousePos;

        if (Input.GetMouseButtonDown(0))
        {
            Collider2D _col = Physics2D.OverlapPoint(_mousePos, interactableLayer);
            if (_col != null)
            {
                MoveTo(_col.GetComponent<Interactable>());
            }
            else 
            {
                if (Physics2D.OverlapCircle(_mousePos, 0.1f, floorLayer) != null)
                { Debug.Log("Clicked on floor"); }
                    
                MoveTo(_mousePos);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            uiManager.IncrementDisplay(1);
        }

        if (transform.position != targetPos)
        {
            float _distance = Vector3.Distance(transform.position, targetPos);
            transform.position = Vector3.Lerp(transform.position, targetPos, Mathf.Clamp(speed * Time.deltaTime / _distance, 0f, 1f));
        } 
        else if (targetInteractable != null)
        {
            targetInteractable.Interact();
            targetInteractable = null;
            animator.Play("HerrBrandIdle");
        }
    }

    public void MoveTo(Interactable _interactable)
    {
        targetInteractable = _interactable;
        targetPos = _interactable.interactSpot.position;
        animator.Play("HerrBrandWalk");
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
