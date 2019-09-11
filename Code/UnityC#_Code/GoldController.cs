using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldController : MonoBehaviour {    
    public int goldAmount = 0;
    public Text goldText;
    public Text gameOverGoldText;

    private void Start () {
        goldAmount = 0;
        UpdateGoldText ();
    }

    public void AddGold (int goldToAdd) {
        goldAmount += goldToAdd;

        if (goldAmount < 0)
            goldAmount = 0;

        UpdateGoldText ();
    }

    public void UpdateGameOverGoldText () {
        gameOverGoldText.text = "YOU GAINED: " + goldAmount.ToString () + " gold";
    }

    private void UpdateGoldText () {
        goldText.text = "Gold: " + goldAmount.ToString ();
    }
}
