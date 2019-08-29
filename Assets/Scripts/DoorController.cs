using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject[] associatedRooms;
    private Animator ani;
    private string dooropenclosestr = "character_nearby";
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.SetActive(true);
    }

    public void activateRooms(bool active)
    {
        foreach (GameObject l in associatedRooms)
        {
            foreach (RoomController rc in l.GetComponents<RoomController>())
            {
                foreach (GameObject go in rc.removeOnLeave)
                {
                    go.SetActive(active);
                }
            }
        }
    }

    public void openDoor()
    {
        activateRooms(true);
        ani.SetBool(dooropenclosestr, true);
    }

    public void closeDoor()
    {
        ani.SetBool(dooropenclosestr, false);
    }
}
