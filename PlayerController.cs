using System.Collections;
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

    public float movementSpeed;
    public float rotationSpeed;

    public DoorController[] nearbyDoors;
    public DoorController startingDC;
    public GameObject[] gameControllers;
    private SphereCollider[] controllerColliders;

    public Transform footprintTransform;
    private float doorNearbyDistance = 4f;
    private List<HandController.handCollision> currentColliding = new List<HandController.handCollision>();

    public SteamVR_Action_Boolean grab;

    // Start is called before the first frame update
    void Start()
    {
        doorUpdate[0] = startingDC;
        rb = GetComponent<Rigidbody>();

        DoorController[] dcs = FindObjectsOfType(typeof(DoorController)) as DoorController[];
        foreach(DoorController dc in dcs)
        {
            dc.activateRooms(false);
        }
        startingDC.activateRooms(true);

        SphereCollider[] colliders = new SphereCollider[gameControllers.Length];
        int i = 0;

        foreach (GameObject o in gameControllers)
        {
            SphereCollider comp = o.GetComponent<SphereCollider>();
            if (comp != null)
            {
                colliders[i] = comp;
                i++;
            }
        }
        controllerColliders = new SphereCollider[i];

        for (int j = 0; j < i; j++)
        {
            controllerColliders[j] = colliders[j];
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkKeys();
        foreach (DoorController dc in nearbyDoors)
        {
            if (Vector3.Distance(transform.position, dc.transform.position) < doorNearbyDistance)
            {
                dc.openDoor();
                atDoor(dc);
            } else
            {
                dc.closeDoor();
            }
        }
    }

    // Called at a fixed time segment
    void FixedUpdate()
    {
        float movementAmount = thumbPos.y;

        Vector3 facing = new Vector3(hmdCam.transform.forward.x, 0f, hmdCam.transform.forward.z);
        Vector3 moveDir = new Vector3(facing.x * movementAmount, 0f, facing.z * movementAmount);
        footprintTransform.rotation = Quaternion.LookRotation(facing * -1);


        if (move)
        {
            transform.Translate(moveDir * movementSpeed * Time.fixedDeltaTime, Space.World);
            //rb.AddForce(moveDir * movementSpeed * Time.fixedDeltaTime);
        }
        if (rotate)
        {
            transform.Rotate(0, rotationSpeed * Time.fixedDeltaTime, 0);
        }
    }

    public void addToCurrentColliding(HandController.handCollision col)
    {
        currentColliding.Add(col);
    }

    public void removeFromCurrentColliding(HandController.handCollision col)
    {
        currentColliding.Remove(col);
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
    public void ActionButtonPressed(SteamVR_Behaviour_Boolean beh, SteamVR_Input_Sources src, bool pressed)
    {
        if (!pressed) return;

        foreach (HandController.handCollision c in currentColliding)
        {
            InteractionInterface interact = c.col.GetComponent<InteractionInterface>();
            if (interact != null)
            {
                interact.interact(true, c.hand);
            }
        }
    }

    public void GrabHeld(SteamVR_Behaviour_Boolean beh, SteamVR_Input_Sources src, bool pressed)
    {
        if (!pressed) return;

        foreach (HandController.handCollision c in currentColliding)
        {
            InteractionInterface interact = c.col.GetComponent<InteractionInterface>();
            if (interact != null)
            {
                interact.grabHeld(c.hand);
            }
        }
    }

    public void GrabLiftUp(SteamVR_Behaviour_Boolean beh, SteamVR_Input_Sources src, bool pressed)
    {
        foreach (HandController.handCollision c in currentColliding)
        {
            InteractionInterface interact = c.col.GetComponent<InteractionInterface>();
            if (interact != null)
            {
                interact.grabRelease(c.hand);
            }
        }
    }

    public void GrabPressedDown(SteamVR_Behaviour_Boolean beh, SteamVR_Input_Sources src, bool pressed)
    {

        foreach (HandController.handCollision c in currentColliding)
        {
            InteractionInterface interact = c.col.GetComponent<InteractionInterface>();
            if (interact != null)
            {
                interact.grabPress(c.hand);
            }
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
