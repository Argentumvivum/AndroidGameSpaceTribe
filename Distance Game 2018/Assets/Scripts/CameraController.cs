using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public DistanceCalculator calculator;

    private Camera camera;
    private Vector3 offset;
    private Vector3 playerYZPosition;
    private bool detach;

    public GameObject righBorder;
    public GameObject leftBorder;

	void Start ()
    {
        camera = gameObject.GetComponent<Camera>();
        offset = transform.position - player.transform.position;
        offset = new Vector3(0, offset.y, offset.z);
        detach = false;
    }

	void Update () {

        detach = calculator.stopped;

        playerYZPosition = new Vector3(0, player.transform.position.y, player.transform.position.z);

        if(!detach)
        {
            transform.position = playerYZPosition + offset * 1.5f;
            SetCameraDistance();
            righBorder.SetActive(true);
            leftBorder.SetActive(true);
        }
	}

    void SetCameraDistance()
    {
        camera.orthographicSize = 7.5f;
    }
}
