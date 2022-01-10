using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic task created for testing.
/// Task is completed when specified key is pressed.
/// Key can be set through inspector.
/// </summary>
[AddComponentMenu("Task Management/Keyboard Input")]
public class TaskKeyInput : Task
{
    [Header("Keyboard Input")]
    public char _inputKey;

    private string _taskDescription;

    // Awake is called either when an active GameObject that contains the
    // script is initialized when a Scene loads, or when a previously inactive
    // GameObject is set to active, or after a GameObject created with 
    // Object.Instantiate is initialized.
    void Awake()
    {
        _taskDescription = string.Format("Press '{0}' key to complete", _inputKey);
    }

    // Update is called once per frame
    protected override void Update()
    {
        // run parent update function
        base.Update();

        // update task
        if (Input.GetKey((KeyCode)_inputKey))
        {
            _taskComplete = true;
        }
    }

    public override string ToString()
    {
        return _taskDescription;
    }
}
