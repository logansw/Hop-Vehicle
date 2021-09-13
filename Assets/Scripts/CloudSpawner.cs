using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cloud;
    [SerializeField] private float spawnRate;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Cloud.cloudCount);
    }

    public void SpawnCloud(float ySpawn)
    {
        if (Cloud.cloudCount <= 1 || Random.Range(0f, 1f) < spawnRate)
        {
            Instantiate(cloud, new Vector2(Random.Range(-15f, 15f), ySpawn + Random.Range(-3f, 3f)), Quaternion.identity);
            if (Random.Range(0f, 10f) < spawnRate)
                Instantiate(cloud, new Vector2(Random.Range(-15f, 15f), ySpawn + Random.Range(-3f, 3f)), Quaternion.identity);
        }
    }
}
