using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerController : MonoBehaviour, InteractionInterface
{
    public enum InteractionType
    {
        DOOR
    }
    public InteractionType interactionType;
    public DoorController associatedDoor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void interact(bool active)
    {
        switch (interactionType)
        {
            case InteractionType.DOOR:
                if (active)
                {
                    associatedDoor.openDoor();
                    foreach (BoxCollider col in associatedDoor.GetComponentsInChildren<BoxCollider>())
                    {
                        col.enabled = false;
                    }
                } else
                {
                    associatedDoor.closeDoor();
                    foreach (BoxCollider col in associatedDoor.GetComponentsInChildren<BoxCollider>())
                    {
                        col.enabled = true;
                    }
                }
                break;
        }
    }
}
