using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using Valve.VR;
using Valve.VR.Extras;

public class PauseMenu : MonoBehaviour
{
    // Public local objects
    public GameObject MainMenu;
    public GameObject OptionMenu;
    public Text VisualQual;
    public SteamVR_Input_Sources leftHand;
    private GameObject player;
    private TaskWatcher taskWatcher;
    private bool can_open = true;

    // Find the player object
    private void Awake()
    {
        // get the task watcher
        if (taskWatcher == null)
        {
            // if there is one
            if (FindObjectsOfType<TaskWatcher>().Length > 0)
            {
                taskWatcher = FindObjectsOfType<TaskWatcher>()[0];
            }
        }

        // Set the player and hand objects
        if (GameObject.Find("Player") != null)
        {
            player = GameObject.Find("Player").transform.GetChild(0).GetChild(3).gameObject;
        }

        // Set the player and hand objects
        if (GameObject.Find("PlayerVRWithHatSocket") != null)
        {
            player = GameObject.Find("PlayerVRWithHatSocket").transform.GetChild(0).GetChild(3).gameObject;
        }

        // Set the player and hand objects
        if (GameObject.Find("PlayerVR") != null)
        {
            player = GameObject.Find("PlayerVR").transform.GetChild(0).GetChild(3).gameObject;
        }

        // Get all pause menu objects
        var Wboards = FindObjectsOfType(typeof(PauseMenu)) as PauseMenu[];
        // For each menu, destroy it until one remains
        for (int i = 0; i < Wboards.Length-1; i++)
        {
            GameObject.Destroy(Wboards[i].transform.gameObject);
        }

        // Change the UI text
        int level = QualitySettings.GetQualityLevel();
        string levelname = "";
        if (level == 0) levelname = "Very Low";
        else if (level == 1) levelname = "Low";
        else if (level == 2) levelname = "Medium";
        else if (level == 3) levelname = "High";
        else if (level == 4) levelname = "Very High";
        else if (level == 5) levelname = "Ultra";

        VisualQual.text = "Visual Quality:\n" + levelname;
    }

    // ====== PUBLIC FUNCTIONS ======
    // Open the main menu
    public void OpenMainMenu()
    {
        OptionMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

    // Open the Options Menu
    public void OpenOptionsMenu()
    {
        MainMenu.SetActive(false);
        OptionMenu.SetActive(true);
        UpdateHandText();
    }

    public void ChangeQuality(bool increase)
    {
        if (increase) QualitySettings.IncreaseLevel();
        else QualitySettings.DecreaseLevel();
        int level = QualitySettings.GetQualityLevel();
        string levelname = "";
        if (level == 0) levelname = "Very Low";
        else if (level == 1) levelname = "Low";
        else if (level == 2) levelname = "Medium";
        else if (level == 3) levelname = "High";
        else if (level == 4) levelname = "Very High";
        else if (level == 5) levelname = "Ultra";

        VisualQual.text = "Visual Quality:\n" + levelname;
    }

    public void SpawnMenu()
    {
        // only continue if this can be opened
        if (!can_open) return;

        if (this.gameObject.transform.GetChild(0).gameObject.activeSelf)
        {
            DeleteMenu();
        }
        else
        {
            // enable the laser pointer
            EnablePointers(true);

            // Set it to be active
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            this.gameObject.transform.GetChild(2).gameObject.SetActive(true);

            // Move the menu in front of the player, then stop tracking the camera
            this.gameObject.transform.SetParent(player.transform);
            this.gameObject.transform.localPosition = new Vector3(0f, 0f, 1f);
            this.gameObject.transform.localEulerAngles = new Vector3(0f, -90f, 0f);
        }
    }

    public void DeleteMenu()
    {
        // disable laser pointers
        EnablePointers(true);
        // Make the board menu inactive
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(2).gameObject.SetActive(false);
    }

    public void ReturnToMain()
    {
        // stop menu opening and delete the menu
        can_open = false;
        DeleteMenu();
        StartCoroutine(WaitToClose(2));
    }

    void Update()
    {
        if (SteamVR_Input.GetStateDown("X", leftHand) || Input.GetKeyDown((KeyCode)'p'))
        {
            Debug.Log("-->Attempting open. . .");
            SpawnMenu();
        }
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            GameObject.Destroy(this);
        }

        // If the player object is null
        if (GameObject.Find("Player") == null)
        {
            // check if PlayerVR is null. If it exists, use it!
            if (GameObject.Find("PlayerVR") != null)
            {
                player = GameObject.Find("PlayerVR").transform.GetChild(0).GetChild(3).gameObject;
            }
            // Otherwise, look for a spawned player instance
            else if (GameObject.Find("PlayerVR(Clone)") != null)
            {
                player = GameObject.Find("PlayerVR(Clone)").transform.GetChild(0).GetChild(3).gameObject;
            }
            else if (GameObject.Find("PlayerVRWithHatSocket") != null)
            {
                player = GameObject.Find("PlayerVRWithHatSocket").transform.GetChild(0).GetChild(3).gameObject;
            }
        }
    }

    public void ChangeDomHand(bool rightHandDom)
    {
        OptionState.RightHandDominant = rightHandDom;
        UpdateHandText();
    }

    // Change how the text is displayed
    private void UpdateHandText()
    {
        // if right had is dominant, show it
        if (OptionState.RightHandDominant)
        {
            OptionMenu.transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<Text>().text = "[  ] Left";
            OptionMenu.transform.GetChild(1).GetChild(2).GetChild(2).GetComponent<Text>().text = "[X] Right";
        }
        // otherwise, show that the left hand is dominant
        else
        {
            OptionMenu.transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<Text>().text = "[X] Left";
            OptionMenu.transform.GetChild(1).GetChild(2).GetChild(2).GetComponent<Text>().text = "[  ] Right";
        }
    }

    private void EnablePointers(bool enable)
    {
        // enable the dominant laser pointer
        GameObject[] activeHudTextTaggedObjects = GameObject.FindGameObjectsWithTag("HudText");
        foreach (GameObject obj in activeHudTextTaggedObjects)
        {
            Debug.Log(obj.transform.parent.parent.tag);
            // if this is a display board, do nothing
            if (obj.transform.parent.parent.tag != "OtherHUDdisplay")
            {
                // add the pointers
                if (enable && obj.transform.parent.parent.parent.parent.parent.GetComponent<SteamVR_LaserPointer>() == null)
                {
                    obj.transform.parent.parent.parent.parent.parent.gameObject.AddComponent<SteamVR_LaserPointer>();
                }
                // destroy the pointers
                else if (obj.transform.parent.parent.parent.parent.parent.GetComponent<SteamVR_LaserPointer>() != null)
                {
                    Destroy(obj.transform.parent.parent.parent.parent.parent.GetComponent<SteamVR_LaserPointer>());

                    // destroy the laser
                    foreach (Transform child in obj.transform.parent.parent.parent.parent.parent)
                    {
                        if (child.gameObject.name.Contains("New Game Object"))
                        {
                            Destroy(child.gameObject);
                        }
                    }
                }
            }
        }
    }

    // replay the current scene
    public void ReplayScene()
    {
        // stop menu opening and delete the menu
        can_open = false;
        DeleteMenu();
        StartCoroutine(WaitToClose(1));
    }

    // Wait a couple seconds, fade out and open a scene
    private IEnumerator WaitToClose(int mode)
    {
        StartCoroutine(taskWatcher.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<FadeUI>().FadeOut(1.5f));
        yield return new WaitForSeconds(1.5f);
        if (mode == 1) Restart();
        if (mode == 2) Close();
    }

    // Restart the current scene
    private void Restart()
    {
        // destroy everything
        foreach (GameObject obj in Object.FindObjectsOfType<GameObject>())
        {
            if (obj != this) GameObject.Destroy(obj);
        }

        // load the main menu, then destroy this
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameObject.Destroy(this);
    }

    // Return to the main menu
    private void Close()
    {
        GameObject.Destroy(GameObject.Find("Player"));
        foreach (GameObject obj in Object.FindObjectsOfType<GameObject>())
        {
            if (obj != this) GameObject.Destroy(obj);
        }
        SceneManager.LoadScene("MainMenu");
        GameObject.Destroy(this);
    }
}
