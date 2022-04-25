using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TaskTutorial : Task 
{
    public HoverButton _hoverButton;

    // Update is called once per frame
    protected override void Update()
    {
        // run parent update function
        base.Update();

        // update task
        if (_hoverButton != null && 
            _hoverButton.buttonDown)
        {
            _taskComplete = true;
        }
    }
}
