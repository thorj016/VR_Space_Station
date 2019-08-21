using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebriObject : MonoBehaviour
{
    public static Vector3 orbitPoint;
    public GameObject debri;
    public Vector3 rotationDir;
    public float rotationSpeed;
    public Vector3 orbitDir;
    public float orbitSpeed;

    public DebriObject( GameObject debri, Vector3 spawnPos, Quaternion localRotation, Transform parent)
    {
        this.debri = Instantiate(debri, spawnPos, localRotation, parent);
        rotationDir = localRotation.eulerAngles;
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
