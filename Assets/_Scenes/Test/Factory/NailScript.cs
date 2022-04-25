using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NailScript : MonoBehaviour
{

    //Lifetime of the nail before it despawns
    public float LifeTime = 20f;

    float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(time < LifeTime)
        {
            time += Time.deltaTime;
        }
        else
        {
            //Despawn the item
            Destroy(gameObject);
        }
    }
}
