using UnityEngine;
using System.Collections;

public class Gold : MonoBehaviour {
    public int pileValue = 5;

    public void DisableGold () {
        this.gameObject.SetActive (false);
    }
}
