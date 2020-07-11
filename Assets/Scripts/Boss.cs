using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    [SerializeField]
    float moveSpeed = 1;
    [SerializeField]
    float rotateSpeed = 1;
    [SerializeField]
    bool isClockWise = true;
    [SerializeField]
    float leftPosX = -2, rightPosX = 2;

    bool isMoveRight = true;
    float timeCount = 0f;

    void Start() {

    }

    void Update() {
        // move
        if (isMoveRight) {
            transform.position = new Vector2(transform.position.x + Time.deltaTime * moveSpeed, transform.position.y);
        }
        else {
            transform.position = new Vector2(transform.position.x - Time.deltaTime * moveSpeed, transform.position.y);
        }
        if (transform.position.x > rightPosX) {
            isMoveRight = false;
        }
        else if(transform.position.x < leftPosX) {
            isMoveRight = true;
        }
        // rotate
        timeCount += Time.deltaTime * rotateSpeed;
        transform.rotation = Quaternion.AngleAxis(timeCount, Vector3.forward);
    }
}
