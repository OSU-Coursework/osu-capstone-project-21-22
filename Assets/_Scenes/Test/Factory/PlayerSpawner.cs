using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject teleportPrefab;
    public GameObject teleportAreaPrefab;
    // Start is called before the first frame update
    void Start()
    { 

        //SteamVR Doesn't play well with reloading scenes. The Scene manager will not
        //destroy the PlayerVR which means another instance gets created every time the
        //scene reloads. To get around this issue we will dynamically spawn the player
        //and ensure that there is only one player. The only problem is that the Teleport
        //assets want the player to already be created in the world and will not recognize
        //a newly spawned player. To fix this we have to spawn the teleport and teleportArea
        //prefabs AFTER the player in order for things to work right. We also need to always
        //spawn them because they do get cleaned up by the scene manager, just not the player.
        //This seems like a very large flaw in the SteamVR unity library.
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if(players.Length == 0)
        {
            GameObject spawn = GameObject.Find("PlayerSpawn");
            if(spawn != null){
                Debug.Log("Spawning player");
                Instantiate(playerPrefab, spawn.transform.position, spawn.transform.rotation);
                Instantiate(teleportPrefab, spawn.transform.position, spawn.transform.rotation);
                GameObject teleportArea = Instantiate(teleportAreaPrefab, new Vector3(20.37f, -21.03f, -49.7f), Quaternion.identity);
                teleportArea.transform.localScale = new Vector3(6.44f, 3.96f, 4.56f);
            }

        }
        else
        {
            GameObject spawn = GameObject.Find("PlayerSpawn");
            if (spawn != null)
            {
                Instantiate(teleportPrefab, spawn.transform.position, spawn.transform.rotation);
                GameObject teleportArea = Instantiate(teleportAreaPrefab, new Vector3(20.37f, -21.03f, -49.7f), Quaternion.identity);
                teleportArea.transform.localScale = new Vector3(6.44f, 3.96f, 4.56f);
            }
        }
        
    }

}
