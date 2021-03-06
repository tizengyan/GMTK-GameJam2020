﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour {
    [SerializeField]
    float coolThreshold = 0.5f, perfectThreshold = 0.2f;
    [SerializeField]
    float unitPerSec = 1f; // 落点x坐标；速度
    [SerializeField]
    KeyCode keyToPress;


    GameObject placeHolder;
    bool canPress = false;
    float activatePosX = -1f;

    void Start() {
        placeHolder = GameObject.FindGameObjectWithTag("Finish");
        if (placeHolder != null) {
            activatePosX = placeHolder.transform.position.x;
        }
    }

    void FixedUpdate ()
    {
        transform.position -= Vector3.right * unitPerSec * Time.fixedDeltaTime;
    }


    void Update() {
        if (Input.GetKeyDown(keyToPress) && canPress) {
            float distance = Mathf.Abs(transform.position.x - activatePosX);
            //Debug.Log("distance = " + distance);
            if (distance > coolThreshold) {
                Debug.Log("Normal Hit!");
                GameManager.GetInstance().HitTempo(0, keyToPress);
                Destroy(gameObject);
            }
            else if (distance > perfectThreshold) {
                Debug.Log("Cool Hit");
                GameManager.GetInstance().HitTempo(1, keyToPress);
                Destroy(gameObject);
            }
            else {
                Debug.Log("Perfect Hit");
                GameManager.GetInstance().HitTempo(2, keyToPress);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Finish") {
            canPress = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Finish") {
            canPress = false;
            Debug.Log("Miss!");
            Destroy(gameObject);
        }
    }


}
