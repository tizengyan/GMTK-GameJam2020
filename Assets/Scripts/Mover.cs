using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {
    [SerializeField]
    private float bottomPosY = -10f, topPosY = 10;
    [SerializeField]
    private float speed = -1f;
    [SerializeField]
    private bool isCycle = true;
    [SerializeField]
    private bool isNeedDelay = true;

    float startDelay = -1f;
    bool gameIsOver = false;

    void Start() {
        startDelay = GameManager.GetInstance().GameStartDelay();
    }
    
    void Update() {
        if (!gameIsOver) {
            if (!isNeedDelay || startDelay < 0) {
                if (transform.position.y > topPosY) {
                    transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
                }
                else if (isCycle) {
                    transform.position = new Vector2(bottomPosY, transform.position.y);
                }
            }
            else {
                startDelay -= Time.deltaTime;
            }
        }
    }

    public void Stop() {
        gameIsOver = true;
    }
}
