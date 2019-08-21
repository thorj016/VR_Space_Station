using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerController : MonoBehaviour
{
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

    private string doorNearbyTriggerStr = "character_nearby";

    // Start is called before the first frame update
    void Start()
    {
        if (!debug)
        {
            debugPlane.SetActive(false);
        } else
        {
            debugPlane.SetActive(true);
        }
        debugText = debugPlane.GetComponentInChildren<TextMesh>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 facing = new Vector3 (hmdCam.transform.forward.x, 0f, hmdCam.transform.forward.z);
        Vector3.Normalize(facing);
        Vector3 moveDir = new Vector3(facing.x * thumbPos.x, 0f, facing.z * thumbPos.y);
        string debugstr = "Facing- X: " + facing.x.ToString("F2") + " Y: " + facing.y.ToString("F2") + " Z: " + facing.z.ToString("F2");
        debugText.text = debugstr;

        checkKeys();

        if (move)
        {
            rb.AddForce(moveDir * movementSpeed * Time.deltaTime);
        }
        if (rotate)
        {
            transform.Rotate(0, Time.deltaTime * rotationSpeed, 0);
        }

        foreach (Animator ani in nearbyDoors)
        {
            if (Vector3.Distance(transform.position, ani.transform.position) < 2.5f)
            {
                ani.SetBool(doorNearbyTriggerStr, true);
            } else
            {
                ani.SetBool(doorNearbyTriggerStr, false);
            }
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
        Debug.Log("updating Thumb Pos");
        this.thumbPos = v1;
    }

    public void UpdateThumbPressDown(SteamVR_Behaviour_Boolean beh, SteamVR_Input_Sources src, bool pressed)
    {
        Debug.Log("entered thumb press down");
        if (pressed)
        {
            move = true;
        }
    }

    public void UpdateThumbLiftUp(SteamVR_Behaviour_Boolean beh, SteamVR_Input_Sources src, bool lift)
    {
        Debug.Log("entered thumb lift up");
        if (!lift)
        {
            move = false;
        }
    }
}
