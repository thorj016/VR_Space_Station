using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject[] associatedLights;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activateLights(bool active)
    {
        foreach (GameObject l in associatedLights)
        {
            l.SetActive(active);
        }
    }
}
