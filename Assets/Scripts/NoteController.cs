using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour {
    [SerializeField]
    float hitThreshold = 0.5f, coolThreshold = 0.25f, perfectThreshold = 0.1f;
    [SerializeField]
    float activatePosX = -1f, stepPerUnit = 0.5f;
    [SerializeField]
    KeyCode keyToPress;

    float BPM;

    void Start() {
        BPM = GameManager.GetInstance().GetBPM();
    }

    void Update() {
        transform.position -= Vector3.right * (1 / stepPerUnit) * Time.deltaTime;
        if (Input.GetKeyDown(keyToPress)) {
            float distance = Mathf.Abs(transform.position.x - activatePosX);
            //Debug.Log("distance = " + distance);
            if (distance < perfectThreshold) {
                Debug.Log("Perfect Hit");
                Destroy(gameObject);
            }
            else if (distance < coolThreshold) {
                Debug.Log("Cool Hit");
                Destroy(gameObject);
            }
            else if (distance < hitThreshold) {
                Debug.Log("Normal Hit!");
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Finish") {
            Debug.Log("Miss!");
            Destroy(gameObject);
        }
    }
}
