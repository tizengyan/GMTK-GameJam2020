using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    [SerializeField]
    float speed = 1;
    [SerializeField]
    float leftPosX = -2, rightPosX = 2;

    bool isMoveRight = true;

    void Start() {

    }

    void Update() {
        if (isMoveRight) {
            transform.position = new Vector2(transform.position.x + Time.deltaTime * speed, transform.position.y);
        }
        else {
            transform.position = new Vector2(transform.position.x - Time.deltaTime * speed, transform.position.y);
        }
        if (transform.position.x > rightPosX) {
            isMoveRight = false;
        }
        else if(transform.position.x < leftPosX) {
            isMoveRight = true;
        }
    }
}
