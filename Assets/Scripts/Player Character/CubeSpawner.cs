using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CubeSpawner : MonoBehaviour
{
    [Header("Prefabs to spawn")]
    public List<GameObject> cubes;

    [Header("Spawn properties")]
    public int numCubes;
    public float range;

    [Header("Respawn cubes")]
    public bool updateCubes = true;
    
    [Header("Spawned Cubes")]
    public List<GameObject> SpawnedCubes;

    GameObject currentCube;

    void Update()
    {
        if (updateCubes)
        {
            //Destroys all spawned cubes
            if(SpawnedCubes.Count > 0)
            {
                for (int i = 0; i < SpawnedCubes.Count; i++)
                {
                    DestroyImmediate(SpawnedCubes[i]);
                }
                SpawnedCubes.Clear();
            }
            

            //Spawns new cubes, adds them to SpawnedCubes
            for (int i = 0; i < numCubes; i++)
            {
                currentCube = Instantiate(
                    cubes[Random.Range(0, cubes.Count)],
                    new Vector3(
                        transform.position.x + Random.Range(-range, range),
                        transform.position.y + Random.Range(-range, range),
                        0),
                    Quaternion.identity);
                currentCube.transform.parent = transform;
                SpawnedCubes.Add(currentCube);
                currentCube.transform.rotation = Random.rotation;
            }

            updateCubes = false;
        }
    }
}
