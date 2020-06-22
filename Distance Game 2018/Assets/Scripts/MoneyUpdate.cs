using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUpdate : MonoBehaviour {

    [SerializeField]
    private Text moneyText;
    private GameManager manager;

	void Start () {
        manager = FindObjectOfType<GameManager>();
        moneyText.text = manager.money.ToString() + " $";
	}
	
	void Update () {
        moneyText.text = manager.money.ToString() + " $";
	}
}
