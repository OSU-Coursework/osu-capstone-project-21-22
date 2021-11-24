using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]  // when selecting objects in the scene editor, this should force
                 //   unity to select the object this script is attached to instead
                 //   of its children (treats it as a root object)

/// <summary>
/// This class will monitor Task objects in a scene and keep
/// track of their completion status.
/// </summary>
[AddComponentMenu("Task Management/Task Watcher")]  // make this script selectable from within editor
public class TaskWatcher : MonoBehaviour
{
    protected Task[] _tasks;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Task Watcher Started");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
