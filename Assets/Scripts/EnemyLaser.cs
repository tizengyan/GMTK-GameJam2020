using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour {
    [SerializeField]
    float appearSpeed = 1f;
    [SerializeField]
    float hintDelay = 2f;
    [SerializeField]
    float selfDestroyDelay = 2f;

    BoxCollider2D collider;

    void Start() {
        collider = GetComponent<BoxCollider2D>();
        collider.enabled = false;
        Color color = gameObject.GetComponent<SpriteRenderer>().color;
        color.a = 0.3f;
        gameObject.GetComponent<SpriteRenderer>().color = color;
        Invoke("BeginShowUp", hintDelay);
    }

    void BeginShowUp() {
        StartCoroutine("ShowUp");
    }

    IEnumerator ShowUp() {
        while (true) {
            Color color = gameObject.GetComponent<SpriteRenderer>().color;
            color.a = Mathf.Clamp(color.a + Time.deltaTime * appearSpeed, 0, 1f);
            gameObject.GetComponent<SpriteRenderer>().color = color;
            if (color.a == 1) {
                collider.enabled = true;
                StartCoroutine("DestroyCount");
                break;
            }
            yield return null;
        }
    }

    IEnumerator DestroyCount() {
        yield return new WaitForSeconds(selfDestroyDelay);
        Destroy(gameObject);
    }

    void Update() {
        
    }
}
