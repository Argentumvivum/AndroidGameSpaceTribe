using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxSpace : MonoBehaviour {

    public float speed = 0.05f;
    public GameObject mainCam;
    private Vector3 lastCamPos;

    private Material _material;
    
    private void Start()
    {
        lastCamPos = mainCam.transform.position;
        _material = GetComponent<Renderer>().material;
    }

    void Update () {
        Vector3 shift = mainCam.transform.position - lastCamPos;
        lastCamPos = mainCam.transform.position;

        Vector2 offset = _material.mainTextureOffset;
        offset.y += shift.y * speed;

        _material.SetTextureOffset("_MainTex", offset);
    }
}
