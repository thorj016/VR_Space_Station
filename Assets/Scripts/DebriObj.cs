using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebriObject
{
    public static Vector3 orbitPoint;
    public GameObject debri;
    public Vector3 rotationDir;
    public float rotationSpeed;
    public Vector3 orbitDir;
    public float orbitSpeed;

    public DebriObject( GameObject debri)
    {
        this.debri = debri;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        debri.transform.Rotate(rotationDir * rotationSpeed * Time.deltaTime);
        debri.transform.RotateAround(orbitPoint, orbitDir, orbitSpeed * Time.deltaTime);
    }
}
