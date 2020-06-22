using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBorders : MonoBehaviour {

    public bool rightBorder;
    public bool leftBorder;

    private float leftLimitation;
    private float rightLimitation;

    private void OnEnable()
    {
        float dist = (transform.position.z - Camera.main.transform.position.z);

        if(leftBorder)
        {
            leftLimitation = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
            transform.position = new Vector3(leftLimitation, 0, 0);
        }

        if(rightBorder)
        {
            rightLimitation = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
            transform.position = new Vector3(rightLimitation, 0, 0);
        }
    }
}
