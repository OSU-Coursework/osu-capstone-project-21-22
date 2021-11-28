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
    protected Task[] _tasks;  // gathered automatically by task watcher on scene initialization
    private int _taskCount;

    public Text _canvasText;
    protected string _canvasTaskList = "";

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    private void OnEnable()
    {
        TaskEventManager.onTaskComplete += UpdateTasks;
    }

    /// <summary>
    /// Sets up properties and collects Tasks in current scene.
    /// </summary>
    public virtual void Initialization()
    {
        //Debug.Log("Task Watcher Started");

        // collect tasks in current scene
        _tasks = Resources.FindObjectsOfTypeAll<Task>();
        _taskCount = _tasks.Length;

        if (_tasks.Length == 0)
        {
            Debug.LogError("No Task objects found in scene");
        }
        else
        {
            foreach (Task t in _tasks)
            {
                //Debug.Log(string.Format("Task Found: {0}", t.ToString()));

                _canvasTaskList += t.ToString() + "\n";
            }

            SetHudText();
        }
    }

    private void UpdateTasks()
    {
        _taskCount = 0;
        _canvasTaskList = "";

        foreach (Task t in _tasks)
        {
            if (!t.TaskComplete)
            {
                // update new count
                _taskCount++;

                // update hud string
                _canvasTaskList += t.ToString() + "\n";
            }
        }

        SetHudText();
    }

    private void SetHudText()
    {
        // set hud text
        if (_canvasText != null)
        {
            string hudText = string.Format("Tasks Remaining: {0}\n\n", _taskCount);
            hudText += _canvasTaskList;

            _canvasText.text = hudText;
        }
        else
        {
            Debug.LogError("ERROR :: no text object assigned to task watcher for hud display!");
        }
    }
}
