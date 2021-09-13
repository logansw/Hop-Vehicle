using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpawner : MonoBehaviour
{
    public GameManager gameManager;
    public Transform playerTrans;
    public CloudSpawner cloudSpawner;
    public GameObject floor;
    public GameObject[] cars;

    private int floorGap = 5;
    private int nextFloorHeight;
    private int floorsUntilSafe;
    private int consecutiveCars;

    // Start is called before the first frame update
    void Start()
    {
        SetupScene();
        cloudSpawner.SpawnCloud(Random.Range(0f, 15f));
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTrans.position.y > nextFloorHeight)
        {
            gameManager.floorsPassed++;
            gameManager.score = gameManager.floorsPassed - 1;
            cloudSpawner.SpawnCloud(playerTrans.position.y + 15);
            nextFloorHeight = gameManager.floorsPassed * floorGap;
            int ySpawn = (gameManager.floorsPassed + 3) * floorGap;
            SpawnFloor(ySpawn);

            if (floorsUntilSafe > 0)
            {
                int randomIndex = Random.Range(0, cars.Length);
                SpawnCar(ySpawn, Random.Range(-5f, 5f), cars[randomIndex]);
                floorsUntilSafe--;
            } else
            {
                consecutiveCars++;
                floorsUntilSafe = consecutiveCars;
            }
        }
    }

    private void SpawnFloor(int ySpawn)
    {
        Instantiate(floor, new Vector3(0, ySpawn, 0), Quaternion.identity);
    }

    private void SpawnCar(int ySpawn, float xSpawn, GameObject car)
    {
        int yHeight = ySpawn + 2;
        Vector3 spawn = new Vector3(xSpawn, yHeight, 0);
        Instantiate(car, spawn, Quaternion.identity);
    }

    public void SetupScene()
    {
        SpawnFloor(0);
        SpawnFloor(floorGap);
        SpawnCar(floorGap, -4, cars[0]);
        SpawnFloor(floorGap * 2);
        SpawnFloor(floorGap * 3);
        SpawnCar(floorGap * 3, Random.Range(-5f, 5f), cars[0]);
        SpawnFloor(floorGap * 4);
        SpawnCar(floorGap * 4, Random.Range(-5f, 5f), cars[0]);
        nextFloorHeight = 5;
        floorsUntilSafe = 0;
        consecutiveCars = 2;
    }
}
