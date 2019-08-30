using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public PlayerController pc;

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
