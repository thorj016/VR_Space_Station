using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerController : MonoBehaviour, InteractionInterface
{
    public enum InteractionType
    {
        DOOR,
        DIMMER
    }
    public InteractionType interactionType;
    public DoorController associatedDoor;
    public Light[] lights;
    private Quaternion startingPos;
    private Quaternion handStartingPos;
    private float maxDegreesRot = 90;
    private bool grabbed = false;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void interact(bool active, Transform caller)
    {
        switch (interactionType)
        {
            case InteractionType.DOOR:
                if (active)
                {
                    associatedDoor.openCloseDoor();
                }
                break;
            case InteractionType.DIMMER:
                break;
        }
    }


    public void grabPress(Transform caller)
    {
        Debug.Log("Grab Pressed");
        switch (interactionType)
        {
            case InteractionType.DIMMER:
                handStartingPos = caller.rotation;
                break;
        }
    }
    public void grabHeld(Transform caller)
    {
        Debug.Log("Grab held");
        switch (interactionType)
        {
            case InteractionType.DIMMER:
                break;
        }
    }
    public void grabRelease(Transform caller)
    {
        Debug.Log("Grab Released");
        switch (interactionType)
        {
            case InteractionType.DIMMER:
                break;
        }
    }
}
