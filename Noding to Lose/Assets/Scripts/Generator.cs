using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{

    public GameObject center;
    private GameObject prefab;
    public int range = 5;
    private Vector3 offset;
    public List<GameObject> indCubes;
    public GameObject[] cubePrefabs;
    private GameObject refCube;
    public Transform indManager;


    private void Awake()
    {
        indCubes = new List<GameObject>();
    }

    void Start ()
    {
        offset = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), Random.Range(1, 3));
        prefab = cubePrefabs[Random.Range(0, cubePrefabs.Length-2)];
        GameObject firstCube = Instantiate(prefab, center.transform.position + offset, Quaternion.identity);
        indCubes.Add(firstCube);
        firstCube.transform.parent = indManager;
        Generate(100);
        InvokeRepeating("CleanUp", 3f, 0.5f);
    }
    
    public void Generate (int count)
    {
        for (int i = 0; i < count; i++)
        {
            GetValues();
            if (refCube == null)
            {
                GetValues();
            }
            GameObject generatedCube = Instantiate(prefab, refCube.transform.position + offset, Quaternion.identity);
            indCubes.Add(generatedCube);
            generatedCube.transform.parent = indManager;
        }
    }

    void GetValues ()
    {
        refCube = indCubes[Random.Range(0, indCubes.Count)];   // Set the reference cube.
        if (refCube == null) // Error check.
        {
            GetValues();
        }
        offset = new Vector3(Random.Range(-range, range), Random.Range(-range, range), Random.Range(-range, 5 + range));     // Set the distance from the reference cube.
        prefab = cubePrefabs[Random.Range(0, cubePrefabs.Length)];        // Choose the node color.
    }

    void CleanUp ()
    {
        foreach (Transform child in indManager)
        {
            if (center == null) { break; }
            if (child == null) { CleanUp(); }
            else
            {
                if (child.transform.position.z < center.transform.position.z - 20)                                      // if a cube is too far behind the CubeManager, move it.
                {
                    child.transform.position = new Vector3(child.transform.position.x, child.transform.position.y, center.transform.position.z + 10 + Random.Range(0, 20));
                }

                else if (child.transform.position.z > center.transform.position.z + 60)                                 // if a cube is too far forward the CubeManager, move it.
                {
                    child.transform.position = new Vector3(child.transform.position.x, child.transform.position.y, center.transform.position.z - 10 - Random.Range(0, 20));
                }

                else if (child.transform.position.x < center.transform.position.x - 30)                                 // if a cube is too far forward the CubeManager, move it.
                {
                    child.transform.position = new Vector3(center.transform.position.x + 10 + Random.Range(0, 20), child.transform.position.y, child.transform.position.z);
                }

                else if (child.transform.position.x > center.transform.position.x + 30)                                 // if a cube is too far forward the CubeManager, move it.
                {
                    child.transform.position = new Vector3(center.transform.position.x - 10 - Random.Range(0, 20), child.transform.position.y, child.transform.position.z);
                }

                if (child.transform.position.y < center.transform.position.y - 30)                                      // if a cube is too far behind the CubeManager, move it.
                {
                    child.transform.position = new Vector3(child.transform.position.x, center.transform.position.y + 10 + Random.Range(0, 20), child.transform.position.z);
                }

                if (child.transform.position.y > center.transform.position.y + 30)                                      // if a cube is too far behind the CubeManager, move it.
                {
                    child.transform.position = new Vector3(child.transform.position.x, center.transform.position.y - 10 - Random.Range(0, 20), child.transform.position.z);
                }
            }
            
        }        
    }
}
