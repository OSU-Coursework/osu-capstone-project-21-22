using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using Valve.VR.InteractionSystem;
using Valve.VR.Extras;

public class MainMenuCanvas : MonoBehaviour
{
    // name of directory containing scenes to be picked up by menu
    private const string sceneDir = "Demo";

    // public local objects
    public GameObject scene_card;
    public GameObject MainMenu;
    public GameObject SceneMenu;
    public GameObject OptionMenu;
    public GameObject SceneLayout;
    public Text VisualQual;

    // private values for pages
    private int page_no = 0;
    private int max_pages;

    private void Awake()
    {
        int level = QualitySettings.GetQualityLevel();
        string levelname = "";
        if (level == 0) levelname = "Very Low";
        else if (level == 1) levelname = "Low";
        else if (level == 2) levelname = "Medium";
        else if (level == 3) levelname = "High";
        else if (level == 4) levelname = "Very High";
        else if (level == 5) levelname = "Ultra";
        VisualQual.text = "Visual Quality:\n" + levelname;
        EnablePointers();
    }

    // Clear the layout children
    private void clearChildren()
    {
        foreach (Transform child in SceneLayout.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        LoadSceneList();
    }

    private void LoadSceneList()
    {
        string scene_dir = (Application.dataPath + "/_Scenes/" + sceneDir);
        string[] scene_list = Directory.GetDirectories(scene_dir);
        float num = (float)scene_list.Length;
        max_pages = (int)(Mathf.Ceil(num / 3.0f) - 1.0f);
        
        // For all scenes, get the scene and image
        for (int i = 0; i < 3; i++)
        {
            // Return if greater than the length
            if ((i + (3 * page_no)) >= scene_list.Length) return;

            // get the path of the scene and the image
            string[] name = Directory.GetFiles(scene_list[i+(3*page_no)], "*.unity");
            string[] image = Directory.GetFiles(scene_list[i+(3 * page_no)], "*.png");
            
            // Create an instance of the scene card
            GameObject card_obj = Instantiate(scene_card);
            card_obj.transform.SetParent(SceneLayout.transform);
            RectTransform cardthing = card_obj.GetComponent<RectTransform>();
            
            // Fix the size and position of the card
            cardthing.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            cardthing.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            
            // Fix the name of the path to be the name of the scene
            string name_name = name[0].Substring((Application.dataPath + "/" + sceneDir + "/").Length);
            string form_name = name_name.Substring(name_name.IndexOf('\\') + 1);
            string final_name = form_name.Substring(form_name.IndexOf('\\') + 1);
            card_obj.transform.GetChild(0).gameObject.GetComponent<Text>().text = final_name.Remove(final_name.IndexOf('.'), 6);

            // a short name for scene reference
            string v_final_name = card_obj.transform.GetChild(0).gameObject.GetComponent<Text>().text;
            // Add this method to the play button
            GameObject play_button = card_obj.transform.GetChild(2).gameObject;
            play_button.GetComponent<Button>().onClick.AddListener(delegate { openScene(v_final_name); });
            play_button.GetComponent<UIElement>().onHandClick.AddListener(delegate { openScene(v_final_name); });

            // Load the image, if one exists
            if (image.Length != 0)
            {
                // Fix the image path to be local
                image[0] = image[0].Replace("\\", "/");
                image[0] = image[0].Substring(image[0].IndexOf(sceneDir));
                image[0] = image[0].Replace(sceneDir + "/", "");
                image[0] = image[0].Remove(image[0].IndexOf('.'), 4);
                var sprite = Resources.Load<Sprite>(image[0]);
                card_obj.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = sprite;
            }
        }
    }


    // ====== PUBLIC FUNCTIONS ======
    // Load the next page
    public void NextPage()
    {
        page_no += 1;
        if (page_no > max_pages)
        {
            page_no = 0;
        }
        clearChildren();
    }

    // Load the last page
    public void LastPage()
    {
        page_no -= 1;
        if (page_no < 0)
        {
            page_no = max_pages;
        }
        clearChildren();
    }

    // Open the scene of the button that was pressed
    public void openScene(string scene_name)
    {
        GameObject.Destroy(GameObject.Find("Player"));
        SceneManager.LoadScene(scene_name);
    }

    // Close the application
    public void quitApp()
    {
        Application.Quit();
    }

    // Open the Scenes menu
    public void OpenScenesMenu()
    {
        page_no = 0;
        MainMenu.SetActive(false);
        OptionMenu.SetActive(false);
        SceneMenu.SetActive(true);
        NextPage();
    }

    // Open the main menu
    public void OpenMainMenu()
    {
        clearChildren();
        page_no = 0;
        SceneMenu.SetActive(false);
        OptionMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

    // Open the Options Menu
    public void OpenOptionsMenu()
    {
        SceneMenu.SetActive(false);
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

    public void ChangeDomHand(bool rightHandDom)
    {
        OptionState.RightHandDominant = rightHandDom;
        UpdateHandText();
    }

    private void EnablePointers()
    {
        // enable the dominant laser pointer
        GameObject[] activeHudTextTaggedObjects = GameObject.FindGameObjectsWithTag("HudText");
        foreach (GameObject obj in activeHudTextTaggedObjects)
        {
            obj.transform.parent.parent.parent.parent.parent.GetComponent<SteamVR_LaserPointer>().enabled = true;
        }
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

}