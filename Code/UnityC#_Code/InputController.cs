using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using SimpleJSON;
using System.IO;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour {
    private string gameDataFileName = "data.json";
    private string gameDataFolderName = "C:/Miner_Arduino/Assets/ExternalData";
    private bool isDebugOn = false;

    public float minLightIntensity = 0f;
    public float maxLightIntensity = 100f;
    public float lightIntensity = 0f;

    public float minTemperatureIntensity = 0f;
    public float maxTemperatureIntensity = 100f;
    public float temperatureIntensity = 0f;

    public int tiltInput = 0;
    public int leftKeyPressed = 0;
    public int rightKeyPressed = 0;

    public PostProcessVolume postProcessVolume;
    public Character character;

    private void Update () {
        if (Input.GetKeyDown (KeyCode.B)) {
            RestartScene ();
        }

        
        if (isDebugOn) {
            if (Input.GetKeyDown (KeyCode.Q)) {
                lightIntensity += 10f;
            }

            if (Input.GetKeyDown (KeyCode.W)) {
                lightIntensity -= 10f;
            }

            if (Input.GetKeyDown (KeyCode.E)) {
                temperatureIntensity += 10f;
            }

            if (Input.GetKeyDown (KeyCode.R)) {
                temperatureIntensity -= 10f;
            }

            leftKeyPressed = 0;
            rightKeyPressed = 0;

            if (Input.GetKey (KeyCode.A)) {
                leftKeyPressed = 1;
            }

            if (Input.GetKey (KeyCode.D)) {
                rightKeyPressed = 1;
            }

            if (Input.GetKeyDown (KeyCode.Z)) {
                tiltInput = tiltInput == 0 ? 1 : 0;
            }
        } 
    }

    private void FixedUpdate () {
        LoadGameData();
        ClampInputs ();
        UpdateLightIntensityShader ();
    }

     public void LoadGameData() {
        string filePath = Path.Combine(gameDataFolderName, gameDataFileName);
        
        if (File.Exists (filePath)) {
            string dataAsJson = File.ReadAllText(filePath);

            var N = JSON.Parse(dataAsJson);
            lightIntensity = N["lightUnits"].AsFloat;
            temperatureIntensity = N["temperature"].AsFloat;
            tiltInput = N["tilt"].AsInt;
            leftKeyPressed = N["turnLeft"].AsInt;
            rightKeyPressed = N["turnRight"].AsInt;
            Debug.Log(temperatureIntensity);

            File.Delete(filePath);
        }     
       
    }

    public void RestartScene () {
        UnityEngine.SceneManagement.SceneManager.LoadScene (0);
    }

    //Make sure that all the inputs are between our min and max values
    private void ClampInputs () {
        lightIntensity = Mathf.Clamp (lightIntensity, minLightIntensity, maxLightIntensity);
        temperatureIntensity = Mathf.Clamp (temperatureIntensity, minTemperatureIntensity, maxTemperatureIntensity);
    }

    private void UpdateLightIntensityShader () {
        LanternEffect lanternEffect;
        postProcessVolume.profile.TryGetSettings (out lanternEffect);

        lanternEffect.enabled.value = character.transform.position.y < -2.5;

        if (lanternEffect) {
            lanternEffect.lightIntensity.value = Mathf.InverseLerp (minLightIntensity, maxLightIntensity, lightIntensity);
            lanternEffect.aspectRatio.value = Camera.main.aspect;            
        }
    }
    
}
