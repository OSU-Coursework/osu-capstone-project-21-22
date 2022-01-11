using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using Valve.VR;

public class PauseMenu : MonoBehaviour
{
    // Public local objects
    public GameObject MainMenu;
    public GameObject OptionMenu;
    public Text VisualQual;
    public SteamVR_Input_Sources leftHand;
    private GameObject player;

    // Find the player object
    private void Awake()
    {
        // Set the player and hand objects
        if (GameObject.Find("Player") != null)
        {
            player = GameObject.Find("Player").transform.GetChild(0).GetChild(3).gameObject;
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
        if (this.gameObject.transform.GetChild(0).gameObject.activeSelf)
        {
            DeleteMenu();
        }
        else
        {
            // Set it to be active
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            this.gameObject.transform.GetChild(2).gameObject.SetActive(true);

            // Move the menu in front of the player, then stop tracking the camera
            this.gameObject.transform.SetParent(player.transform);
            this.gameObject.transform.localPosition = new Vector3(0f, 0f, 0.6f);
            this.gameObject.transform.localEulerAngles = new Vector3(0f, -90f, 0f);
            this.gameObject.transform.SetParent(null);
        }
    }

    public void DeleteMenu()
    {
        // Make the board menu inactive
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(2).gameObject.SetActive(false);
    }

    public void ReturnToMain()
    {
        DeleteMenu();
        GameObject.Destroy(GameObject.Find("Player"));
        foreach (GameObject obj in Object.FindObjectsOfType<GameObject>())
        {
            if (obj != this) GameObject.Destroy(obj);
        }
        SceneManager.LoadScene("MainMenu");
        GameObject.Destroy(this);
    }

    void Update()
    {
        if (SteamVR_Input.GetStateDown("X", leftHand))
        {
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
        }
    }
}
