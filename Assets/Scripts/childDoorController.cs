using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class childDoorController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Animator ani = GetComponentInParent<Animator>();
        TouchDoor dc = GetComponentInParent<TouchDoor>();
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("glass_door_opened"))
        {
            dc.closeDoor();
        }
        else if (ani.GetCurrentAnimatorStateInfo(0).IsName("glass_door_closed"))
        {
            dc.openDoor();
        }
    }
}
