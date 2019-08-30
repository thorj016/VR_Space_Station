using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class DemoActionSetInput : MonoBehaviour
{

    public SteamVR_ActionSet actionSetEnable;

    void Start()
    {
        actionSetEnable.Activate();
    }
}