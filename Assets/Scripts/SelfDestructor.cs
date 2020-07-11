using UnityEngine;

public class SelfDestructor : MonoBehaviour {
    [SerializeField]
    float selfDestroyDelay = 1f;

    void Start() {
        Invoke("DestroySelf", selfDestroyDelay);
    }

    void DestroySelf() {
        Destroy(gameObject);
    }
}
