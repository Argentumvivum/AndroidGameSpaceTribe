using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceCalculator : MonoBehaviour {

    public Transform player;
    public Text distanceText;
    public bool stopped;
    public SpawnerController spawner;
    public GameObject distanceUI;
    private GameManager manager;
    private float currentDistance;
    public GameObject redLine;
    private LineRenderer lineRender;
    private bool distanceSaved;
    private Rigidbody2D playerRB;

    private void Start()
    {
        distanceSaved = false;
        lineRender = redLine.GetComponent<LineRenderer>();
        manager = FindObjectOfType<GameManager>();
        SetRedLine();
        stopped = true;
        currentDistance = 1.0f;
        distanceText.color = Color.clear;
        distanceUI.SetActive(false);
        playerRB = player.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate () {

        float distance = Vector3.Distance(transform.position, player.position);

        if(distance >= 10000.0f)
        {
            stopped = true;
            playerRB.velocity = Vector2.zero;
            playerRB.constraints = RigidbodyConstraints2D.FreezeAll;
            manager.LoadEnding();
        }

        else if (distance > 15f)
        {
            if (currentDistance < distance)
            {
                distanceUI.SetActive(true);
                distanceText.color = Color.black;
                currentDistance = distance;
                distanceText.text = distance.ToString("0.0") + "m";
                stopped = false;
                distanceSaved = false;
                if (!spawner.spawnerActive)
                    spawner.StartSpawning();
            }
            else
            {
                stopped = true;
                if(!distanceSaved)
                    manager.SaveDistance(currentDistance);
                distanceSaved = true;
                //if (spawner.spawnerActive)
                    //spawner.StopSpawning();
            } 
        }
	}

    private void SetRedLine()
    {
        redLine.SetActive(true);
        var record = manager.LoadDistance();
        if (record > 0.0f)
        {
            lineRender.SetPosition(0, new Vector3(-10, record, -1));
            lineRender.SetPosition(1, new Vector3(10, record, -1));
        }
    }
}
