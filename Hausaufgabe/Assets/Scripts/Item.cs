using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    public override void Interact()
    {
        Player.Instance.SetCurrentItem(this);
    }
}
