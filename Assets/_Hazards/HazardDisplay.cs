using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HazardDisplay : MonoBehaviour
{
    private GameObject _currZone = null;
    public GameObject currZone
    {
        get { return _currZone; }
        set { _currZone = value; }
    }

    private void Awake()
    {
        // set the rendering camera
        GetComponent<Canvas>().worldCamera = transform.parent.GetComponent<Camera>();
    }

    public void Activate(GameObject zone, int msg)
    {
        // if another zone is in use, return
        if (_currZone != null) return;


        // set the zone and make this object active
        _currZone = zone;
        transform.GetChild(0).gameObject.SetActive(true);

        // MESSAGE 1: Hands + Head
        if (msg == 0) transform.GetChild(0).GetChild(2).GetComponent<Text>().text = "LEAVE THE YELLOW AREA";
        if (msg == 1) transform.GetChild(0).GetChild(2).GetComponent<Text>().text = "REMOVE YOUR HANDS\nOR HEAD FROM THE YELLOW AREA";
    }

    public void Deactivate()
    {
        // clear the zone and make this object inactive
        _currZone = null;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void SetMessage(int msg)
    {
        if (msg == 0) transform.GetChild(0).GetChild(2).GetComponent<Text>().text = "LEAVE THE YELLOW AREA";
        if (msg == 1) transform.GetChild(0).GetChild(2).GetComponent<Text>().text = "REMOVE YOUR HANDS OR\nHEAD FROM THE YELLOW AREA";
    }
}
