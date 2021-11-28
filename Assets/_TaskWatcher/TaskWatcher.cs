using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Text _canvasText;
    protected string _canvasTaskList = "";

    protected Task[] _tasks;  // gathered automatically by task watcher on scene initialization

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Sets up properties and collects Tasks in current scene.
    /// </summary>
    public virtual void Initialization()
    {
        Debug.Log("Task Watcher Started");

        // collect tasks in current scene
        _tasks = Resources.FindObjectsOfTypeAll<Task>();

        if (_tasks.Length == 0)
        {
            Debug.Log("No Task objects found in scene");
        }
        else
        {
            foreach (Task t in _tasks)
            {
                Debug.Log(string.Format("Task Found: {0}", t.TaskName));

                _canvasTaskList += t.TaskName + "\n";
            }
        
            // set hud text
            if (_canvasText != null)
            {
                _canvasText.text = _canvasTaskList;
            }
            else
            {
                Debug.LogError("ERROR :: no text object assigned to task watcher for hud display!");
            }
        }
    }
}
