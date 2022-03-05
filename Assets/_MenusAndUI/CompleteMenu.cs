using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
//using Valve.VR;
//using Valve.VR.InteractionSystem;
//using Valve.VR.Extras;

public class CompleteMenu : MonoBehaviour
{
    // Public local objects
    //public TaskWatcher taskWatcher;
    //public bool active = false;

    //// task count
    //private int taskCnt;

    //[Header("Ending UI Colors")]
    //public Color great = new Color(0.5f, 1f, 0.5f, 1.0f);
    //public Color good = new Color(0.3f, 1f, 0.0f, 1.0f);
    //public Color average = new Color(9f, 0.9f, 0.4f, 1.0f);
    //public Color bad = new Color(1f, 0.6f, 0.5f, 1.0f);


    //// Find the player object
    //private void Awake()
    //{
    //    // Get all pause menu objects
    //    var Wboards = FindObjectsOfType(typeof(PauseMenu)) as PauseMenu[];
    //    // For each menu, destroy it until one remains
    //    for (int i = 0; i < Wboards.Length - 1; i++)
    //    {
    //        GameObject.Destroy(Wboards[i].transform.gameObject);
    //    }

    //    // get the task watcher
    //    if (taskWatcher == null)
    //    {
    //        taskWatcher = FindObjectsOfType<TaskWatcher>()[0];
    //    }
    //}


    //// return a value for the performance based on time
    //private int GetTimeQuality(int min, int sec)
    //{
    //    if (!taskWatcher.useTimes) return -1;
    //    // if times are better than the great time
    //    if (taskWatcher.greatTime.minutes >= min &&
    //        taskWatcher.greatTime.seconds >= sec)
    //    {
    //        return 1;
    //    }
    //    // if the times are better than the good time
    //    else if (taskWatcher.goodTime.minutes >= min &&
    //            taskWatcher.goodTime.seconds >= sec)
    //    {
    //        return 2;
    //    }
    //    // if the times are better than the average time
    //    else if (taskWatcher.averageTime.minutes >= min &&
    //            taskWatcher.averageTime.seconds >= sec)
    //    {
    //        return 3;
    //    }
    //    // if the times are not good
    //    else
    //    {
    //        return 4;
    //    }
    //}


    //public void SpawnMenu()
    //{
    //    active = true;

    //    // destroy the pause menu
    //    if (FindObjectsOfType<PauseMenu>().Length > 0) Destroy(FindObjectsOfType<PauseMenu>()[0].gameObject);

    //    // get the task count
    //    if (taskWatcher != null)
    //    {
    //        taskCnt = taskWatcher.TaskCount;
    //    }

    //    int minutes = (int)Mathf.Floor((float)Time.timeSinceLevelLoad / (float)60);
    //    int seconds = (int)Mathf.Floor((float)Time.timeSinceLevelLoad % (float)60);
    //    int time_status = GetTimeQuality(minutes, seconds);

    //    // set the UI color
    //    Image col = transform.GetChild(1).GetChild(1).GetComponent<Image>();
    //    Text perf = transform.GetChild(1).GetChild(3).GetComponent<Text>();
    //    if (time_status == -1)
    //    {
    //        perf.text = "Good work!";
    //    }
    //    if (time_status == 1)
    //    {
    //        col.color = great;
    //        perf.text = "Performance:\nExcellent";
    //    }
    //    else if (time_status == 2)
    //    {
    //        col.color = good;
    //        perf.text = "Performance:\nGood";
    //    }
    //    else if (time_status == 3)
    //    {
    //        col.color = average;
    //        perf.text = "Performance:\nAverage";
    //    }
    //    else if (time_status == 4)
    //    {
    //        col.color = bad;
    //        perf.text = "Performance:\nPoor";
    //    }

    //    // Set the stats text
    //    transform.GetChild(1).GetChild(2).GetComponent<Text>().text = taskCnt.ToString() + " tasks in\n";
    //    transform.GetChild(1).GetChild(2).GetComponent<Text>().text += minutes.ToString() + " min, " + seconds.ToString() + " sec";

    //    // disable all teleportation
    //    foreach (Teleport tele in FindObjectsOfType<Teleport>())
    //    {
    //        Debug.Log(tele.gameObject.name);
    //        Destroy(tele.gameObject);
    //    }

    //    // move the player
    //    GameObject fadecam = FindObjectsOfType<SteamVR_Fade>()[0].gameObject;
    //    GameObject player = FindObjectsOfType<Player>()[0].gameObject;
    //    GameObject target = GameObject.FindGameObjectsWithTag("CompleteTeleport")[0].gameObject;

    //    // remove global lighting
    //    GameObject.Find("Directional Light").GetComponent<Light>().color = Color.black;
    //    GameObject.Find("Directional Light").GetComponent<Light>().intensity = 0;

    //    // fade out the camera, then move the player to the target position
    //    SteamVR_Fade.View(Color.black, 0);
    //    player.transform.position = target.transform.position;
    //    fadecam.transform.position = new Vector3(target.transform.position.x, fadecam.transform.position.y, target.transform.position.z);
    //    // rotate the room to face the player
    //    target.transform.parent.eulerAngles = new Vector3(0, fadecam.transform.eulerAngles.y, 0);
    //    // fade back in
    //    SteamVR_Fade.View(Color.clear, 0.5f);


    //    // enable the laser pointer
    //    EnablePointers(true);

    //    // Set it to be active
    //    this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    //    this.gameObject.transform.GetChild(1).gameObject.SetActive(true);

    //    // Move the menu in front of the player, always
    //    //this.gameObject.transform.SetParent(FindObjectsOfType<SteamVR_Fade>()[0].transform);
    //    //this.gameObject.transform.localPosition = new Vector3(0f, -0.2f, 1.3f);
    //    //this.gameObject.transform.localEulerAngles = new Vector3(0f, -90f, 0f);
    //}

    //public void DeleteMenu()
    //{
    //    // disable laser pointers
    //    EnablePointers(true);
    //    // Make the board menu inactive
    //    this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    //    this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
    //}

    //public void ReturnToMain()
    //{
    //    // delete the menu and the player
    //    DeleteMenu();
    //    GameObject.Destroy(FindObjectsOfType<Player>()[0]);

    //    // destroy everything EXCEPT tasks
    //    foreach (GameObject obj in Object.FindObjectsOfType<GameObject>())
    //    {
    //        if (obj != this) GameObject.Destroy(obj);
    //    }

    //    // load the main menu, then destroy this
    //    SceneManager.LoadScene("MainMenu");
    //    GameObject.Destroy(this);
    //}


    //// replay the current scene
    //public void ReplayScene()
    //{
    //    // delete the menu and the player
    //    DeleteMenu();
    //    GameObject.Destroy(FindObjectsOfType<Player>()[0]);

    //    // destroy everything EXCEPT tasks
    //    foreach (GameObject obj in Object.FindObjectsOfType<GameObject>())
    //    {
    //        if (obj != this) GameObject.Destroy(obj);
    //    }

    //    // load the main menu, then destroy this
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //    GameObject.Destroy(this);
    //}

    //void Update()
    //{
    //    // if this is in the main menu, destroy this
    //    if (SceneManager.GetActiveScene().name == "MainMenu")
    //    {
    //        GameObject.Destroy(this);
    //    }
    //}

    //private void EnablePointers(bool enable)
    //{
    //    // enable the dominant laser pointer
    //    GameObject[] activeHudTextTaggedObjects = GameObject.FindGameObjectsWithTag("HudText");
    //    foreach (GameObject obj in activeHudTextTaggedObjects)
    //    {
    //        Debug.Log(obj.transform.parent.parent.tag);
    //        // if this is a display board, do nothing
    //        if (obj.transform.parent.parent.tag != "OtherHUDdisplay")
    //        {
    //            // add the pointers
    //            if (enable && obj.transform.parent.parent.parent.parent.parent.GetComponent<SteamVR_LaserPointer>() == null)
    //            {
    //                obj.transform.parent.parent.parent.parent.parent.gameObject.AddComponent<SteamVR_LaserPointer>();
    //            }
    //            // destroy the pointers
    //            else if (obj.transform.parent.parent.parent.parent.parent.GetComponent<SteamVR_LaserPointer>() != null)
    //            {
    //                Destroy(obj.transform.parent.parent.parent.parent.parent.GetComponent<SteamVR_LaserPointer>());

    //                // destroy the laser
    //                foreach (Transform child in obj.transform.parent.parent.parent.parent.parent)
    //                {
    //                    if (child.gameObject.name.Contains("New Game Object"))
    //                    {
    //                        Destroy(child.gameObject);
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}
}
