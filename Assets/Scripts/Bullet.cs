using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField]
    float speed = 2;

    void Start() {
        Invoke("DestroySelf", 10);
    }

    void Update() {
        transform.Translate(-Vector3.up * Time.deltaTime);
    }

    void DestroySelf() {
        Destroy(gameObject);
    }
}
