using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour {

    public float spawnTimerStart;
    public float spawnTimerRepeat;
    public float obstacleSlowDown;
    public float minObstacleVelocity;
    public int maxObstacles;
    public int obstaclesCount;

    public bool spawnerActive;

    public Rigidbody2D playerRB;

    public int[] chanceToSpawn;
    public GameObject[] objectToSpawn;
    public GameObject[] spawners;
    
    private GameObject spawnedObject;
    private GameObject spawner;

    private void Start()
    {
        spawnerActive = false;
        obstaclesCount = 0;

        foreach (GameObject spawner in spawners)
        {
            spawner.transform.position = new Vector3(spawner.transform.position.x + 0.025f, 10, 10);
        }
    }

    public void StartSpawning()
    {
        InvokeRepeating("SpawnObject", spawnTimerStart, spawnTimerRepeat);
        spawnerActive = true;
    }

    public void StopSpawning()
    {
        CancelInvoke("SpawnObject");
        spawnerActive = false;
    }
	
	void SpawnObject()
    {
        if(obstaclesCount < maxObstacles)
        {
            spawner = PickSpawner();
            spawnedObject = Instantiate(PickObject(), spawner.transform.position, spawner.transform.rotation);

            if (playerRB.velocity.y - obstacleSlowDown <= minObstacleVelocity)
            {
                spawnedObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, minObstacleVelocity), ForceMode2D.Impulse);
                obstaclesCount += 1;
            }
            else
            {
                spawnedObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, playerRB.velocity.y - obstacleSlowDown), ForceMode2D.Impulse);
                obstaclesCount += 1;
            }
        }
    }

    GameObject PickObject()
    {
        int r = Random.Range(0, 100);
        
        int cumulative = 0;

        GameObject selectedObject = null;

        for(int i = 0; i < objectToSpawn.Length; i++)
        {
            cumulative += chanceToSpawn[i];
            if(r < cumulative)
            {
                selectedObject = objectToSpawn[i];
                break;
            }
        }

        return selectedObject;
    }

    GameObject PickSpawner()
    {
        return spawners[Random.Range(0, spawners.Length)];
    }
}
