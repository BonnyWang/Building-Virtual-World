using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveSpawner : MonoBehaviour
{
    public GameObject hivePrefab; // Drag your Hive prefab here in the Inspector
    public float spawnCheckDelay = 2.0f; // Time delay between spawn checks

    private Camera mainCamera;

    // Define a buffer distance from the edges of the screen
    public float buffer = 1.0f; // Adjust as needed

    // Define a minimum safe distance from the Beekeeper
    public float minDistanceFromBeekeeper = 2.0f; // Adjust as needed

    void Start()
    {
        mainCamera = Camera.main;
        InvokeRepeating("SpawnHiveIfNone", 3f, spawnCheckDelay);
    }

    void SpawnHiveIfNone()
    {
        if (GameObject.FindGameObjectWithTag("Hive") == null) // Assuming your hive prefab has a tag "Hive"
        {
            SpawnHive();
        }
    }

    void SpawnHive()
    {
        GameObject beekeeper = GameObject.FindGameObjectWithTag("Keeper"); // Assuming your Beekeeper has a tag "Beekeeper"

        Vector3 spawnPosition;
        int maxAttempts = 10; // Max attempts to find a good spawn position
        int attempts = 0;

        do
        {
            spawnPosition = new Vector3(
                Random.Range(mainCamera.ViewportToWorldPoint(new Vector3(0 + buffer, 0)).x, mainCamera.ViewportToWorldPoint(new Vector3(1 - buffer, 0)).x),
                Random.Range(mainCamera.ViewportToWorldPoint(new Vector3(0, 0 + buffer)).y, mainCamera.ViewportToWorldPoint(new Vector3(0, 1 - buffer)).y),
                0
            );

            attempts++;

        } while (Vector3.Distance(spawnPosition, beekeeper.transform.position) < minDistanceFromBeekeeper && attempts < maxAttempts);

        Instantiate(hivePrefab, spawnPosition, Quaternion.identity);
    }



}
