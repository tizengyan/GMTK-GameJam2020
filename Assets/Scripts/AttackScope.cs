using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScope : MonoBehaviour {
    [SerializeField]
    float selfDestroyDelay = 0.2f;

    void Start() {
        Invoke("DestroySelf", selfDestroyDelay);
    }

    void DestroySelf() {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Bullet") {
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Bullet") {
            Destroy(collision.gameObject);
        }
    }
}
