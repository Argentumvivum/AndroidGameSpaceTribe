using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public PlayerCalculator calc;
    public GameObject botBorder;
    public GameObject upgradeMenu;
    private GameManager manager;
    [SerializeField]
    private GameMenuOptionsController gameOptions;
    private UpgradeController upgrades;
    private LaunchPlayer launcher;
    private Rigidbody2D playerRB;
    public GameObject[] uiElements;

    public GameObject rightBorder;
    public GameObject leftBorder;

    private float moveSpeed;

    private void OnLevelWasLoaded()
    {
        upgrades = FindObjectOfType<UpgradeController>();
        moveSpeed = calc.playerMoveSpeed * upgrades.moveSpeedUpgradeValue;
    }

    void Start () {
        playerRB = GetComponent<Rigidbody2D>();
        launcher = FindObjectOfType<LaunchPlayer>();
        manager = FindObjectOfType<GameManager>();
        manager.gamePaused = false;
        upgrades.DeactivateShop();
        if(!manager.seenTutorial)
            gameOptions.ShowTutorial();
    }
	
	void FixedUpdate () {
        
		if(Input.touchCount == 1 && launcher.playerLaunched)
        {
            Vector2 move = Vector2.zero;

            Touch touch = Input.GetTouch(0);

            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, 0, 0));

            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                Vector2 velocity = (touchPosition - transform.position);
                move.x = velocity.normalized.x;
                if (transform.position.x != touchPosition.x)
                    playerRB.AddForce(move * moveSpeed);
            }
        }

        if(Input.touchCount == 0)
        {
            playerRB.velocity = new Vector2(0, playerRB.velocity.y);
        }

        if(leftBorder.activeInHierarchy)
        {
            if (transform.position.x <= leftBorder.transform.position.x + 1 && playerRB.velocity.normalized.x < 0)
            {
                playerRB.velocity = new Vector2(0, playerRB.velocity.y);
            }
        }

        if(rightBorder.activeInHierarchy)
        {
            if(transform.position.x >= rightBorder.transform.position.x - 1 && playerRB.velocity.normalized.x > 0)
            {
                playerRB.velocity = new Vector2(0, playerRB.velocity.y);
            }
        }

        if (botBorder.transform.position.y > transform.position.y && !manager.gamePaused)
        {
            manager.PauseGame();
            manager.SaveGame();
            foreach(var element in uiElements)
            {
                element.SetActive(false);
            }
            upgradeMenu.SetActive(true);
        }
	}
}
