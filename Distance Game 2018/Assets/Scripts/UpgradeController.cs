using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeController : MonoBehaviour {

    public List<string> lvlPrices;

    public GameObject tooltip;

    public float jumpMultiplier;
    public float slowDownMultiplier;
    public float speedUpMultiplier;
    public float movesSpeedMultiplier;
    public float gravityMultiplier;

    public float jumpUpgradeValue;
    public float slowDownUpgradeValue;
    public float speedUpUpgradeValue;
    public float moveSpeedUpgradeValue;
    public float gravityUpgradeValue;

    private GameManager manager;
    public InputField field;

    public string JumpTooltipText;
    public string SlowTooltipText;
    public string MoveTooltipText;
    public string GravityTooltipText;
    public string SpeedTooltipText;

    [SerializeField]
    private Text jumpText;
    [SerializeField]
    private Text slowDownText;
    [SerializeField]
    private Text speedUpText;
    [SerializeField]
    private Text moveSpeedText;
    [SerializeField]
    private Text gravityText;
    [SerializeField]
    private Text jumpPriceText;
    [SerializeField]
    private Text slowPriceText;
    [SerializeField]
    private Text speedPriceText;
    [SerializeField]
    private Text movePriceText;
    [SerializeField]
    private Text gravityPriceText;

    public int maxUpgradeLvl = 5;

    private void Awake()
    {
        manager = FindObjectOfType<GameManager>();

        jumpText.text = manager.bounceUpgrade.ToString() + "/5";
        slowDownText.text = manager.slowDownUpgrade.ToString() + "/5";
        speedUpText.text = manager.speedUpUpgrade.ToString() + "/5";
        moveSpeedText.text = manager.moveSpeedUpgrade.ToString() + "/5";
        gravityText.text = manager.gravityUpgrade.ToString() + "/5";

        moveSpeedUpgradeValue = 1 + manager.moveSpeedUpgrade * movesSpeedMultiplier;
        jumpUpgradeValue = 1 + manager.bounceUpgrade * jumpMultiplier;
        slowDownUpgradeValue = manager.slowDownUpgrade * slowDownMultiplier;
        gravityUpgradeValue = manager.gravityUpgrade * gravityMultiplier;
        speedUpUpgradeValue = 1 + manager.speedUpUpgrade * speedUpMultiplier;

        jumpPriceText.text = lvlPrices[manager.bounceUpgrade];
        slowPriceText.text = lvlPrices[manager.slowDownUpgrade];
        speedPriceText.text = lvlPrices[manager.speedUpUpgrade];
        movePriceText.text = lvlPrices[manager.moveSpeedUpgrade];
        gravityPriceText.text = lvlPrices[manager.gravityUpgrade];
    }

    public void DeactivateShop()
    {
        gameObject.SetActive(false);
    }

    public void JumpTooltip()
    {
        tooltip.SetActive(true);
        field.text = JumpTooltipText;
    }

    public void MoveTooltip()
    {
        tooltip.SetActive(true);
        field.text = MoveTooltipText;
    }

    public void SlowTooltip()
    {
        tooltip.SetActive(true);
        field.text = SlowTooltipText;
    }

    public void GravityTooltip()
    {
        tooltip.SetActive(true);
        field.text = GravityTooltipText;
    }

    public void SpeedTooltip()
    {
        tooltip.SetActive(true);
        field.text = SpeedTooltipText;
    }

    public void CloseTooltip()
    {
        tooltip.SetActive(false);
    }

    private int GetPrice(string stringToParse)
    {
        return int.Parse(Regex.Match(stringToParse, @"\d+").Value);
    }

    public void UpgradeMoveSpeed()
    {
        if(manager.moveSpeedUpgrade < maxUpgradeLvl && manager.money >= GetPrice(lvlPrices[manager.moveSpeedUpgrade]))
        {
            manager.money -= GetPrice(lvlPrices[manager.moveSpeedUpgrade]);
            manager.moveSpeedUpgrade++;
            moveSpeedText.text = manager.moveSpeedUpgrade.ToString() + "/5";
            movePriceText.text = lvlPrices[manager.moveSpeedUpgrade];
            manager.SaveGame();
        }
        moveSpeedUpgradeValue = 1 + manager.moveSpeedUpgrade * movesSpeedMultiplier;
    }

    public void UpgradeBounceForce()
    {
        if (manager.bounceUpgrade < maxUpgradeLvl && manager.money >= GetPrice(lvlPrices[manager.bounceUpgrade]))
        {
            manager.money -= GetPrice(lvlPrices[manager.bounceUpgrade]);
            manager.bounceUpgrade++;
            jumpText.text = manager.bounceUpgrade.ToString() + "/5";
            jumpPriceText.text = lvlPrices[manager.bounceUpgrade];
            manager.SaveGame();
        }
        jumpUpgradeValue = 1 + manager.bounceUpgrade * jumpMultiplier;
    }

    public void UpgradeSlowDown()
    {
        if (manager.slowDownUpgrade < maxUpgradeLvl && manager.money >= GetPrice(lvlPrices[manager.slowDownUpgrade]))
        {
            manager.money -= GetPrice(lvlPrices[manager.slowDownUpgrade]);
            manager.slowDownUpgrade++;
            slowDownText.text = manager.slowDownUpgrade.ToString() + "/5";
            slowPriceText.text = lvlPrices[manager.slowDownUpgrade];
            manager.SaveGame();
        }
        slowDownUpgradeValue = manager.slowDownUpgrade * slowDownMultiplier;
    }

    public void UpgradeGravityScale()
    {
        if (manager.gravityUpgrade < maxUpgradeLvl && manager.money >= GetPrice(lvlPrices[manager.gravityUpgrade]))
        {
            manager.money -= GetPrice(lvlPrices[manager.gravityUpgrade]);
            manager.gravityUpgrade++;
            gravityText.text = manager.gravityUpgrade.ToString() + "/5";
            gravityPriceText.text = lvlPrices[manager.gravityUpgrade];
            manager.SaveGame();
        }
        gravityUpgradeValue = manager.gravityUpgrade * gravityMultiplier;
    }

    public void UpgradeSpeedUp()
    {
        if (manager.speedUpUpgrade < maxUpgradeLvl && manager.money >= GetPrice(lvlPrices[manager.speedUpUpgrade]))
        {
            manager.money -= GetPrice(lvlPrices[manager.speedUpUpgrade]);
            manager.speedUpUpgrade++;
            speedUpText.text = manager.speedUpUpgrade.ToString() + "/5";
            speedPriceText.text = lvlPrices[manager.speedUpUpgrade];
            manager.SaveGame();
        }
        speedUpUpgradeValue = 1 + manager.speedUpUpgrade * speedUpMultiplier;
    }

    public void Restart()
    {
        manager.SaveGame();
        manager.UnPauseGame();
        manager.StartGame();
    }
}
