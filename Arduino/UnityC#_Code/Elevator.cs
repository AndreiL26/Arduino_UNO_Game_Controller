using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {
    public InputController inputController;
    
    public bool hasPlayerAttached;

    public float minY = -180f;
    private float maxY;

    public float speed = 3f;

    private void Start () {
        maxY = this.transform.position.y;
    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.tag == "Player") {
            hasPlayerAttached = true;
        }
    }

    private void OnTriggerExit2D (Collider2D other) {
        if (other.tag == "Player") {
            hasPlayerAttached = false;
        }
    }

    private void Update () {
        if (hasPlayerAttached == false)
            return;

        float movementDirection = inputController.tiltInput == 1 ? -1 : 1;
        float currentY = this.transform.position.y + movementDirection * speed * Time.deltaTime;

        this.GetComponent<Rigidbody2D> ().MovePosition (new Vector3 (this.transform.position.x,
            Mathf.Clamp (currentY, minY, maxY),
            this.transform.position.z));
 
    }
}
