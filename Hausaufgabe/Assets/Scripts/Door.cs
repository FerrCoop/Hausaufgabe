using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Interactable
{
    [SerializeField] private string targetLevel;

    public override void Interact()
    {
        SceneManager.LoadScene(targetLevel);
    }
}
