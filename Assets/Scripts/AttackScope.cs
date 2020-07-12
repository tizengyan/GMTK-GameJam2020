using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScope : MonoBehaviour {
    bool canAttack = true;
    float attackInterval = 0.15f;

    void Start() {

    }

    void SetCanAttack() {
        canAttack = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Bullet") {
            GameManager.GetInstance().HitBullet();
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Enemy" && canAttack) {
            canAttack = false;
            Invoke("SetCanAttack", attackInterval);
            GameManager.GetInstance().HitBoss();
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "Bullet") {
            GameManager.GetInstance().HitBullet();
            Destroy(collision.gameObject);
        }
    }
}
