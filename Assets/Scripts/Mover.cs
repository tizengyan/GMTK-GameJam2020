using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {
    [SerializeField]
    private float beginPosY = -10f, endPosY = 10;
    [SerializeField]
    private float speed = -1f;
    [SerializeField]
    private bool isCycle = true;

    bool gameIsOver = true;

    void Start() {
        
    }
    
    void Update() {
        if (!gameIsOver) {
            if (transform.position.y > endPosY) {
                transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
            }
            else if (isCycle) {
                transform.position = new Vector2(transform.position.x, beginPosY);
            }
        }
    }

    public void StartGame() {
        gameIsOver = false;
    }

    public void Stop() {
        gameIsOver = true;
    }
}
