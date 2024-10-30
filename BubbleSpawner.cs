using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FallJam
{
public class BubbleSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;      // Prefab to be spawned
    public float spawnInterval = 2f;      // Time between spawns in seconds
    private Transform[] spawnPoints;      // Array to hold the positions of the children

    void Start()
    {
        // Get all child transforms
        int childCount = transform.childCount;
        spawnPoints = new Transform[childCount];

        for (int i = 0; i < childCount; i++)
        {
            spawnPoints[i] = transform.GetChild(i);
        }

        // Start the spawning coroutine
        StartCoroutine(SpawnPrefabsContinuously());
    }

    IEnumerator SpawnPrefabsContinuously()
    {
        while (true)
        {
            SpawnAtRandomPosition();
            yield return new WaitForSeconds(spawnInterval);  // Wait for the specified interval
        }
    }

    void SpawnAtRandomPosition()
    {
        if (spawnPoints.Length == 0 || prefabToSpawn == null)
        {
            Debug.LogWarning("No spawn points or prefab to spawn!");
            return;
        }

        // Choose a random spawn point
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform chosenPoint = spawnPoints[randomIndex];

        // Instantiate prefab at the chosen spawn point
        Instantiate(prefabToSpawn, chosenPoint.position, Quaternion.identity);
    }
}
}
