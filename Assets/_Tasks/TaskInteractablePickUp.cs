using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Valve.VR.InteractionSystem;

/// <summary>
/// Basic task created for testing.
/// Task is completed when specified key is pressed.
/// Key can be set through inspector.
/// </summary>
[AddComponentMenu("Task Management/Interactable Pick Up")]
public class TaskInteractablePickUp : Task
{
    public GameObject _interactableGameObject;
    //private Interactable _interactable;

    private string _taskDescription;

    // Awake is called either when an active GameObject that contains the
    // script is initialized when a Scene loads, or when a previously inactive
    // GameObject is set to active, or after a GameObject created with 
    // Object.Instantiate is initialized.
    void Awake()
    {
        // get handle for steamvr interactable script
        //_interactable = _interactableGameObject.GetComponent<Interactable>();

        _taskDescription = string.Format("Pick up {0}", _interactableGameObject.name);
    }

    // Update is called once per frame
    protected override void Update()
    {
        // run parent update function
        base.Update();

        // update task
        //Note from Allan- commenting out to be able to build
        //if (_interactable != null) // && 
        //    //_interactable.attachedToHand != null)
        //{
        //    _taskComplete = true;
        //}
    }

    public override string ToString()
    {
        return _taskDescription;
    }
}
