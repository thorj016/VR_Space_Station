﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerController : MonoBehaviour
{

    private DoorController[] doorUpdate = new DoorController[2];

    Vector2 thumbPos = new Vector2(1f,1f);
    bool move = false;
    bool rotate = false;
    Rigidbody rb;
    public Camera hmdCam;
    
    public GameObject debugPlane;
    TextMesh debugText;

    public float movementSpeed;
    public float rotationSpeed;
    public bool debug = false;

    public Animator[] nearbyDoors;
    public DoorController startingDC;

    private string doorNearbyTriggerStr = "character_nearby";
    private float doorNearbyDistance = 4f;

    // Start is called before the first frame update
    void Start()
    {
        doorUpdate[0] = startingDC;
        if (!debug)
        {
            debugPlane.SetActive(false);
        } else
        {
            debugPlane.SetActive(true);
        }
        debugText = debugPlane.GetComponentInChildren<TextMesh>();
        rb = GetComponent<Rigidbody>();

        DoorController[] dcs = FindObjectsOfType(typeof(DoorController)) as DoorController[];
        foreach(DoorController dc in dcs)
        {
            dc.activateRooms(false);
        }
        startingDC.activateRooms(true);
    }

    // Update is called once per frame
    void Update()
    {
        checkKeys();
        foreach (Animator ani in nearbyDoors)
        {
            if (Vector3.Distance(transform.position, ani.transform.position) < doorNearbyDistance)
            {
                DoorController dc = ani.gameObject.GetComponent<DoorController>();
                if (dc != null)
                {
                    atDoor(dc);
                }
                ani.SetBool(doorNearbyTriggerStr, true);
            } else
            {
                ani.SetBool(doorNearbyTriggerStr, false);
            }
        }
    }

    // Called at a fixed time segment
    void FixedUpdate()
    {
        float movementAmount = thumbPos.y;

        Vector3 facing = new Vector3(hmdCam.transform.forward.x, 0f, hmdCam.transform.forward.z);
        facing = facing.normalized;
        Vector3 moveDir = new Vector3(facing.x * movementAmount, 0f, facing.z * movementAmount);
        string debugstr = "Facing- X: " + facing.x.ToString("F2") + " Y: " + facing.y.ToString("F2") + " Z: " + facing.z.ToString("F2");
        debugText.text = debugstr;


        if (move)
        {
            rb.AddForce(moveDir * movementSpeed * Time.fixedDeltaTime);
        }
        if (rotate)
        {
            transform.Rotate(0, rotationSpeed * Time.fixedDeltaTime, 0);
        }
    }

    void atDoor(DoorController dc)
    {
        if (doorUpdate[0] == null)
        {
            doorUpdate[0] = dc;
            doorUpdate[0].activateRooms(true);
            return;
        }

        if (doorUpdate[1] == null)
        {
            if (doorUpdate[0] == dc)
            {
                return;
            } else
            {
                doorUpdate[1] = dc;
                doorUpdate[0].activateRooms(false);
                doorUpdate[1].activateRooms(true);
            }
        }

        if (doorUpdate[1] == dc)
        {
            return;
        } else {
            doorUpdate[0] = doorUpdate[1];
            doorUpdate[1] = dc;
            doorUpdate[0].activateRooms(false);
            doorUpdate[1].activateRooms(true);
        }
    }

    void checkKeys()
    {

        // Key Downs
        if (Input.GetKeyDown(KeyCode.W))
        {
            move = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            move = true;
            movementSpeed *= -1;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            rotate = true;
            rotationSpeed *= -1;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rotate = true;
        }

        //Key Ups
        if (Input.GetKeyUp(KeyCode.W))
        {
            move = false;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            move = false;
            movementSpeed *= -1;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            rotate = false;
            rotationSpeed *= -1;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            rotate = false;
        }



    }

    public void UpdateThumbPos(Valve.VR.SteamVR_Behaviour_Vector2 beh, SteamVR_Input_Sources src, Vector2 v1, Vector2 v2)
    {
        this.thumbPos = v1;
    }

    public void UpdateThumbPressDown(SteamVR_Behaviour_Boolean beh, SteamVR_Input_Sources src, bool pressed)
    {
        if (pressed)
        {
            move = true;
        }
    }

    public void UpdateThumbLiftUp(SteamVR_Behaviour_Boolean beh, SteamVR_Input_Sources src, bool lift)
    {
        if (!lift)
        {
            move = false;
        }
    }
}
