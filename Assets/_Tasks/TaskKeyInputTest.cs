using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic task created for testing.
/// Task is completed when specified key is pressed.
/// Key can be set through inspector.
/// </summary>
[AddComponentMenu("Task Management/Keyboard Input")]
public class TaskKeyInputTest : Task
{
    public char _inputKey;

    private string _taskDescription;

    // Start is called before the first frame update
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
