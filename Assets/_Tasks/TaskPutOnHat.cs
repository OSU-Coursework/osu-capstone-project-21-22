using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskPutOnHat : Task
{
    private Socket _hatSocket;
    private GameObject _playerHatSocketObject;
    private string _hatSocketObjectName = "HatOnlySocket";
    private string _taskDescription = "Put on a hat!";

    // Awake is called either when an active GameObject that contains the
    // script is initialized when a Scene loads, or when a previously inactive
    // GameObject is set to active, or after a GameObject created with 
    // Object.Instantiate is initialized.
    void Awake()
    {
        // this is bad practice but this task is
        //   designed to require this object.
        _playerHatSocketObject = GameObject.Find(_hatSocketObjectName);
        
        // do some error handling since we're trying to
        //   find this object with a string lookup.
        if (_playerHatSocketObject == null)
        {
            Debug.LogError("ERROR :: " + 
                            this.GetType().ToString() + 
                            " :: GameObject with name " +
                            _hatSocketObjectName +
                            " not found!");
        }
        else
        {
            _hatSocket = _playerHatSocketObject.GetComponent<Socket>();

            // do some error handling here too just in case.
            if (_hatSocket == null)
            {
                Debug.LogError("ERROR :: " + 
                                this.GetType().ToString() + 
                                " :: GameObject with name " +
                                _hatSocketObjectName +
                                " is missing a Socket!");
            }
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        // run parent update function
        base.Update();

        // update task
        if (_hatSocket.HoldingSocketable)
        {
            _taskComplete = true;
        }
    }

    public override string ToString()
    {
        return _taskDescription;
    }
}
