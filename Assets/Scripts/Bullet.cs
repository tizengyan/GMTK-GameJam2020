using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField]
    float speed = 2;
    [SerializeField]
    string[] triggerTagNames;

    void Start() {

    }

    void Update() {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //if (collision.tag == "Edge" || collision.tag == "Player") {
        //    Destroy(gameObject);
        //}
        foreach(var name in triggerTagNames) {
            if (collision.tag == name) {
                Destroy(gameObject);
            }
        }
    }
}
