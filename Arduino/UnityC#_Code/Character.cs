using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour {
    public InputController inputController;
    public GoldController goldController;
    private Rigidbody2D myRigidbody;
    public float speed = 3f;
    private bool isGameOver = false;

    public float warm = 0f;
    public Image heatImage;

    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private Canvas gameCanvas;

    private void Start () {
        myRigidbody = this.GetComponent<Rigidbody2D> ();
        warm = inputController.maxTemperatureIntensity;

        gameCanvas.enabled = true;
        gameOverCanvas.enabled = false;
    }
    

    private void Update () {
        if (isGameOver)
            return;

        float movementDirection = 0f;
        bool leftKey = inputController.leftKeyPressed == 1 ? true : false;
        bool rightKey = inputController.rightKeyPressed == 1 ? true : false;
        bool both = leftKey && rightKey;

        if (leftKey) {
            movementDirection = -1f;
            SetCharacterOrientation (true);
        }

        if (rightKey) {
            movementDirection = 1f;
            SetCharacterOrientation (false);
        }

        if (both)
            movementDirection = 0f;

        this.transform.position += new Vector3 (movementDirection * speed * Time.deltaTime, 0f, 0f);

        warm -= inputController.maxTemperatureIntensity * 0.25f * Time.deltaTime;
        warm += inputController.temperatureIntensity * Time.deltaTime; 
        warm = Mathf.Clamp (warm, inputController.minTemperatureIntensity, inputController.maxTemperatureIntensity);
        UpdateWarmProcent ();

        if (warm <= 0f) {
            GameOver ();
        }
    }    

    private void GameOver () {        
        if (isGameOver == false)
            isGameOver = true;

        gameCanvas.enabled = false;
        gameOverCanvas.enabled = true;
        goldController.UpdateGameOverGoldText ();
    }

    private void SetCharacterOrientation (bool leftOrRight) {
        this.transform.localScale = new Vector3 (leftOrRight ? -1f : 1f, 1f, 1f);
    }

    private void UpdateWarmProcent () {
        float percent = Mathf.InverseLerp (inputController.minTemperatureIntensity, inputController.maxTemperatureIntensity, warm);
        heatImage.fillAmount = percent;
    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.tag == "Gold") {
            Gold gold = other.GetComponent<Gold> ();            
            goldController.AddGold (gold.pileValue);
            gold.DisableGold ();
        }
    }
}
