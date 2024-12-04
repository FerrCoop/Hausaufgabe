using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : Interactable
{
    public BinType binType;

    public enum BinType
    {
        Green,
        Restmull,
        Yellow,
        Blue,
        Brown
    }

    public override void Interact()
    {
        Item _currentItem = Player.Instance.CurrentItem;
        if (_currentItem != null && _currentItem.GetType() == typeof(Refuse))
        {
            Refuse _refuse = (Refuse)_currentItem;
            if (_refuse.binType == this.binType)
            {
                Destroy(_currentItem.gameObject);
                Player.Instance.SetCurrentItem(null);
            }
            else
            {
                Debug.Log("Incorrect!");
            }
        }
    }
}
