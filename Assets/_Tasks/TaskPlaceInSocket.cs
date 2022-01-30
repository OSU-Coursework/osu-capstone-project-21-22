using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskPlaceInSocket : Task
{
    public Socket _socket;

    private string _taskDescription;

    // Awake is called either when an active GameObject that contains the
    // script is initialized when a Scene loads, or when a previously inactive
    // GameObject is set to active, or after a GameObject created with 
    // Object.Instantiate is initialized.
    void Awake()
    {
        _taskDescription = string.Format("Place an object in the {0}", _socket.name);
    }

    // Update is called once per frame
    protected override void Update()
    {
        // run parent update function
        base.Update();

        // update task
        if (_socket.HoldingSocketable)
        {
            _taskComplete = true;
        }
    }

    public override string ToString()
    {
        return _taskDescription;
    }
}
