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
        StartCoroutine("RotatePattern2");
    }

    IEnumerator RotatePattern1() {
        while (true) {
            isClockWise = !isClockWise;
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator RotatePattern2() {
        while (true) {
            float delay = Random.Range(1f, 5f);
            isClockWise = !isClockWise;
            yield return new WaitForSeconds(delay);
        }
    }

    void Update() {
        if (GameManager.GetInstance().IsGameStarted) {
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
            else if (transform.position.x < leftPosX) {
                isMoveRight = true;
            }
            // rotate by z axis
            if (isClockWise)
                timeCount += Time.deltaTime * rotateSpeed;
            else
                timeCount -= Time.deltaTime * rotateSpeed;
            //transform.rotation = Quaternion.AngleAxis(timeCount, Vector3.forward);
            transform.rotation = Quaternion.Euler(0f, 0f, timeCount);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "PlayerBullet") {
            GameManager.GetInstance().HitBoss();
        }
    }
}
