﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public PlayerController pc;
    public bool holdingItem = false;
    Transform heldItem;
    Transform heldItemParent;
    static int positionsToTrack = 10;
    Vector3[] heldPositions = new Vector3[positionsToTrack];

    public struct handCollision {
        public Collider col;
        public Transform hand;

        public handCollision(Collider col, Transform obj)
        {
            this.col = col;
            this.hand = obj;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (holdingItem)
        {
            for (int i = 0; i < positionsToTrack-1; i++)
            {
                heldPositions[i] = heldPositions[i + 1];
            }
            heldPositions[positionsToTrack - 1] = heldItem.transform.position;
        }
    }

    public void holdItem(Transform item)
    {
        this.heldItem = item;
        holdingItem = true;
        heldItemParent = item.transform.parent;
    }

    public Transform releaseItem()
    {
        Transform item = heldItem;
        this.heldItem = null;


        return item;
    }

    private void OnTriggerEnter(Collider other)
    {
        pc.addToCurrentColliding(new handCollision(other, transform));
    }

    private void OnTriggerExit(Collider other)
    {
        pc.removeFromCurrentColliding(new handCollision(other, transform));
    }
}
