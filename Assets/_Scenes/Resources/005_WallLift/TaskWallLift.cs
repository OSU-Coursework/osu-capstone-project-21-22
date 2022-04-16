using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskWallLift : Task
{
    public WallLiftAndStand _wall;
    void Awake()
    {
        if (_wall == null)
        {

            Debug.LogError("ERROR :: " +
                            this.GetType().ToString() +
                            " :: contains no reference to a valid wall object!");
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        // run parent update function
        base.Update();

        if (_wall.Standing)
        {
            _taskComplete = true;
        }
    }
}
