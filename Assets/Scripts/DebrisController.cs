using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisController : MonoBehaviour
{
    public GameObject[] normalAsteroids;
    public GameObject[] electricAsteroids;
    public GameObject[] lavaAsteroids;
    public GameObject[] rockyAsteroids;
    public GameObject[] spaceJunk;
    public GameObject earth;

    public int minDistance;
    public int midDistance;
    public int maxDistance;
    public float minScale;
    public float maxScale;
    public float spaceJunkScale;

    public int numOfAsteroids;
    public int numOfSpaceJunk;

    public float astMinRotationSpeed;
    public float astMaxRotationSpeed;
    public float junkMinRotationSpeed;
    public float junkMaxRotationSpeed;

    public float astMinOrbitSpeed;
    public float astMaxOrbitSpeed;
    public float junkMinOrbitSpeed;
    public float junkMaxOrbitSpeed;

    private DebriObject[] debri;
    public float earthScale;

    // Start is called before the first frame update
    void Start()
    {
        DebriObject.orbitPoint = this.transform.position;
        debri = new DebriObject[numOfAsteroids+numOfSpaceJunk+1];
        Vector3 v1 = Random.insideUnitSphere;
        Vector3 v2 = v1 * (maxDistance - midDistance) * 3;
        Vector3 v3 = v1.normalized;
        Vector3 v4 = v3 * midDistance * 3;
        Vector3 spawnPos = v4 + v2;
        Quaternion rot = Random.rotation;
        GameObject newobj = Instantiate(earth, spawnPos, rot, transform);
        debri[numOfAsteroids + numOfSpaceJunk] = new DebriObject(newobj);
        debri[numOfAsteroids + numOfSpaceJunk].orbitDir = Random.rotation.eulerAngles;
        debri[numOfAsteroids + numOfSpaceJunk].orbitSpeed = Random.Range(astMinOrbitSpeed, astMaxOrbitSpeed) /10;
        debri[numOfAsteroids + numOfSpaceJunk].debri.transform.localScale *= earthScale;

        for (int i = 0; i < numOfAsteroids; i++)
        {
            GameObject ast;
            float r = Random.value;
            if (r < .05)
            {
                ast = lavaAsteroids[(int)Random.Range(1, lavaAsteroids.Length - 1)];
            } else if ( r < .15) {
                ast = electricAsteroids[(int)Random.Range(1, electricAsteroids.Length - 1)];
            }  else if (r < .6) {
                ast = rockyAsteroids[(int)Random.Range(1, rockyAsteroids.Length - 1)];
            } else {
                ast = normalAsteroids[(int)Random.Range(1, normalAsteroids.Length - 1)];
            }
            v1 = Random.insideUnitSphere;
            v2 = v1 * (midDistance - minDistance);
            v3 = v1.normalized;
            v4 = v3 * minDistance;
            spawnPos = v4 + v2;
            rot = Random.rotation;
            newobj = Instantiate(ast, spawnPos, rot, this.transform);
            debri[i] = new DebriObject(newobj);
            debri[i].rotationSpeed = Random.Range(astMinRotationSpeed, astMaxRotationSpeed);
            debri[i].orbitDir = Random.rotation.eulerAngles;
            debri[i].orbitSpeed = Random.Range(astMinOrbitSpeed, astMaxOrbitSpeed);
            debri[i].debri.transform.localScale *= Random.Range(minScale,maxScale);
         }

        for (int i = 0; i < numOfSpaceJunk; i++)
        {
            GameObject junk = spaceJunk[(int)Random.Range(1.1f, spaceJunk.Length -1)];

            v1 = Random.insideUnitSphere;
            v2 = v1 * (maxDistance - midDistance);
            v3 = v1.normalized;
            v4 = v3 * midDistance;
            spawnPos = v4 + v2;
            rot = Random.rotation;
            newobj = Instantiate(junk, spawnPos, rot, this.transform);
            debri[i + numOfAsteroids] = new DebriObject(newobj);
            debri[i + numOfAsteroids].rotationSpeed = Random.Range(junkMinRotationSpeed, junkMaxRotationSpeed);
            debri[i + numOfAsteroids].orbitDir = Random.rotation.eulerAngles;
            debri[i + numOfAsteroids].orbitSpeed = Random.Range(junkMinOrbitSpeed, junkMaxOrbitSpeed);
            debri[i + numOfAsteroids].debri.transform.localScale *= (Random.Range(minScale, maxScale) * spaceJunkScale);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (DebriObject debri in this.debri)
        {
            debri.Update();
        }
    }
}
