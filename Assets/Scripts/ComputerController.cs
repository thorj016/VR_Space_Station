﻿using System.Collections;
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
    private float curRotation = 0;
    private bool grabbed = false;
    public Transform dial;
    // Start is called before the first frame update
    void Start()
    {
        if (dial != null)
        {
            startingPos = dial.rotation;
        }
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
        switch (interactionType)
        {
            case InteractionType.DIMMER:
                handStartingPos = caller.localRotation;
                break;
        }
    }
    public void grabHeld(Transform caller)
    {
        switch (interactionType)
        {
            case InteractionType.DIMMER:
                float handAngel = handStartingPos.eulerAngles.z - caller.eulerAngles.z;
                handStartingPos = caller.localRotation;
                curRotation += handAngel;
                
                dial.rotation = Quaternion.AngleAxis(curRotation, dial.forward);

                Debug.Log(handAngel + " | " + curRotation);
                break;
        }
    }
    public void grabRelease(Transform caller)
    {
        switch (interactionType)
        {
            case InteractionType.DIMMER:
                break;
        }
    }
}
