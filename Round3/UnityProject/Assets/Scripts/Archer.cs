using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    public GameObject arrowPrefab;
    public float spawnInterval = 2.0f; // Time between arrow spawns

    private float timer;

    private void Start()
    {
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnArrow();
            timer = 0f;
        }
    }

    void SpawnArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        ArrowBehavior arrowBehavior = arrow.GetComponent<ArrowBehavior>();

        //if (arrowBehavior != null)
        //{
            GameObject beekeeper = GameObject.FindWithTag("Keeper");
            //if (beekeeper != null)
            //{
                arrowBehavior.SetTarget(beekeeper.transform.position);
        //    }
        //}
    }

}
