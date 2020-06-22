using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderController : MonoBehaviour
{
    public SpawnerController spawner;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            spawner.obstaclesCount -= 1;
            Destroy(collision.gameObject);
        }
    }
}
