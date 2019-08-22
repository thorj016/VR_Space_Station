using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDoor : MonoBehaviour
{
    private Animator ani;
    private SphereCollider handleCol;
    private string touchTriggerString = "character_nearby";
    private bool doorOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        ani = this.GetComponent<Animator>();
        handleCol = this.GetComponentInChildren<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openDoor()
    {
        ani.SetBool(touchTriggerString, true);
    }

    public void closeDoor()
    {
        ani.SetBool(touchTriggerString, false);
    }
}
