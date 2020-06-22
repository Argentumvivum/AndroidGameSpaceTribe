using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LaunchPlayer : MonoBehaviour {
    
    public GameObject player;
    public PlayerCalculator playerCalculator;
    public Sprite playerSprite;
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    public float stage1Multiplier;
    public float stage2Multiplier;
    public float stage3Multiplier;

    private float multiplier;
    private float jumpForce;
    private float gravityScale;
    private Animator animator;
    private Rigidbody2D playerRB;
    private UpgradeController upgrades;
    private GameManager manager;
    public bool playerLaunched;

    private void OnLevelWasLoaded()
    {
        manager = FindObjectOfType<GameManager>();
        upgrades = FindObjectOfType<UpgradeController>();
        jumpForce = playerCalculator.launchForce * upgrades.jumpUpgradeValue;
        gravityScale = playerCalculator.playerGravityScale - upgrades.gravityUpgradeValue;
    }

    private void Start()
    {
        playerLaunched = false;
        animator = gameObject.GetComponent<Animator>();
        playerRB = player.GetComponent<Rigidbody2D>();
        multiplier = playerCalculator.jumpMultiplier;
    }

    public void Update()
    {
        if(!playerLaunched && manager.seenTutorial && !manager.gamePaused)
        {
            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                int touchID = touch.fingerId;

                if (touch.phase == TouchPhase.Ended)
                {
                    if(EventSystem.current.IsPointerOverGameObject(touch.fingerId) ||
                        EventSystem.current.currentSelectedGameObject != null)
                    {
                        return;
                    }

                    Transform parentTransform = player.transform.parent;

                    player.transform.parent = null;
                    animator.Rebind();
                    player.transform.parent = parentTransform;
                    playerRB.AddForce(Vector2.up * jumpForce * multiplier);
                    playerRB.gravityScale = gravityScale;

                    animator.SetBool("Release", true);
                    playerRB.GetComponent<SpriteRenderer>().sprite = playerSprite;
                    playerRB.GetComponent<ConstantForce2D>().torque = 55f;
                    audioSource.PlayOneShot(audioClips[1], 0.5f);

                    playerLaunched = !playerLaunched;
                }
            }
        }
    }

    public void StageOne()
    {
        multiplier = stage1Multiplier;
    }

    public void StageTwo()
    {
        multiplier = stage2Multiplier;
    }

    public void StageThree()
    {
        multiplier = stage3Multiplier;
    }

    public void BendSound()
    {
        audioSource.PlayOneShot(audioClips[0], 0.5f);
    }
}
