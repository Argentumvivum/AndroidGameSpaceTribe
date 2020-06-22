using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagesController : MonoBehaviour {

    public GameObject player;
    public GameObject[] layers;
    public float[] stageHeights;
    public float sunYToSubstract;

    private GameObject activeLayer;
    private GameObject previousLayer;
    private Vector3[] layersPositionOffsets;
    private Vector3 currentOffset;
    private Vector3 playerYZPosition;
    private Vector3 sunOffset;
    private float sunYOffset;
    private int currentStage;
    
	void Start () {
        activeLayer = layers[0];
        previousLayer = null;
        layersPositionOffsets = new Vector3[layers.Length];
        currentStage = 0;

        for(int i = 0; i < layers.Length; i++)
        {
            layersPositionOffsets[i] = layers[i].transform.position;
        }

        currentOffset = layersPositionOffsets[0];
	}
	
	void Update () {

        playerYZPosition = new Vector3(0, player.transform.position.y, 5);

        sunYOffset = ((player.transform.position.y / stageHeights[layers.Length - 1]) * 100) * sunYToSubstract / 100;

        sunOffset.Set(0, sunYOffset, 0);

        int stage = GetStage(player.transform.position.y);

        if (stage != currentStage && stage != layers.Length - 1)
            SetStage(stage);

        if (player.transform.position.y >= activeLayer.transform.position.y)
        {
            activeLayer.transform.position = playerYZPosition;
        }

        for(var i = 0; i < layers.Length; i++)
        {
            if(layers[i] != activeLayer && layers[i] != previousLayer)
            {
                layers[i].transform.position = activeLayer.transform.position + layersPositionOffsets[i] - currentOffset;
            }
            if(i == layers.Length - 1)
            {
                layers[i].transform.position = activeLayer.transform.position + layersPositionOffsets[i] - currentOffset - sunOffset;
            }
        }
        
        currentStage = stage;
	}

    int GetStage(float height)
    {
        int stage = 0;

        for(int i = 0; i < stageHeights.Length; i++)
        {
            if(height >= stageHeights[i])
            {
                stage = i;
            }
        }

        return stage;
    }

    void SetStage(int stage)
    {
        previousLayer = activeLayer;
        activeLayer = layers[stage];

        for (int i = 0; i < layers.Length; i++)
        {
            layersPositionOffsets[i] = layersPositionOffsets[i] - layersPositionOffsets[stage - 1];
        }

        currentOffset = layersPositionOffsets[stage];
    }
}
