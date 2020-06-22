using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleController : MonoBehaviour {

    public LayerMask obstacleMask;
    public SpawnerController spawner;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] audioClips;
    private GameManager manager;
    private Rigidbody2D playerRB;
    private UpgradeController upgrades;
    private float slowDown;
    private float speedUp;
    
    private bool hitObstacle;

    private void OnLevelWasLoaded()
    {
        upgrades = FindObjectOfType<UpgradeController>();
        slowDown = upgrades.slowDownUpgradeValue;
        speedUp = upgrades.speedUpUpgradeValue;
    }

    void Start () {
        manager = FindObjectOfType<GameManager>();
        hitObstacle = false;
        playerRB = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () {

        hitObstacle = Physics2D.OverlapCircle(transform.position, 0.1f, obstacleMask);
        
        if (hitObstacle)
        {
            ObstacleStats hitObjectStats = Physics2D.OverlapCircle(transform.position, 0.1f, obstacleMask).GetComponent<ObstacleStats>();

            manager.money += hitObjectStats.moneyToAdd;

            if(hitObjectStats.velocityToAdd > 0)
            {
                audioSource.PlayOneShot(audioClips[0], 0.1f);
                playerRB.AddForce(new Vector2(0, hitObjectStats.velocityToAdd * speedUp), ForceMode2D.Impulse);
            }
                
            if(hitObjectStats.velocityToAdd < 0)
            {
                audioSource.PlayOneShot(audioClips[1], 0.1f);
                playerRB.AddForce(new Vector2(0, hitObjectStats.velocityToAdd - hitObjectStats.velocityToAdd * slowDown), ForceMode2D.Impulse);
            }

            spawner.obstaclesCount -= 1;
            Destroy(Physics2D.OverlapCircle(transform.position, 0.1f, obstacleMask).gameObject);
        }
	}
}
