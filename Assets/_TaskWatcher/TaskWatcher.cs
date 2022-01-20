using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public bool _executeTasksInOrder = false;
    private int _activeTaskIndex = 0;

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
        _tasks = FindObjectsOfType<Task>();
        _taskCount = _tasks.Length;

        if (_tasks.Length == 0)
        {
            Debug.LogError("No Task objects found in scene");
        }
        else
        {
            // sort tasks by their order in the scene hierarchy
            _tasks = _tasks.OrderBy(t => t.transform.GetSiblingIndex()).ToArray();

            foreach (Task t in _tasks)
            {
                //Debug.Log(string.Format("Task Found: {0}", t.ToString()));

                if (_executeTasksInOrder)
                {
                    if (t == _tasks[_activeTaskIndex])
                    {
                        _canvasTaskList += t.ToString() + "\n";
                    }
                    else  // disable all task objects but the active one
                    {
                        t.gameObject.SetActive(false);
                    }
                }
                else  // tasks are not ordered, display them all
                {
                    _canvasTaskList += t.ToString() + "\n";
                }
            }

            SetHudText();
        }
    }

    private void UpdateTasks()
    {
        _taskCount = 0;
        _canvasTaskList = "";

        if (_executeTasksInOrder && _tasks[_activeTaskIndex].TaskComplete)
        {
            // increment task tracking index when task is completed
            _activeTaskIndex = Mathf.Min(_activeTaskIndex + 1, _tasks.Length - 1);

            // activate new task
           _tasks[_activeTaskIndex].gameObject.SetActive(true);
        }

        foreach (Task t in _tasks)
        {
            if (!t.TaskComplete)
            {
                // update new count
                _taskCount++;

                // update hud string
                if (_executeTasksInOrder)
                {
                    if (t == _tasks[_activeTaskIndex])
                    {
                        _canvasTaskList += t.ToString() + "\n";
                    }
                }
                else  // tasks are not ordered, display them all
                {
                    _canvasTaskList += t.ToString() + "\n";
                }
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
