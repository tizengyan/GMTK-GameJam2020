using UnityEngine;

public class SelfDestructor : MonoBehaviour {
    [SerializeField]
    float selfDestroyDelay = 1f;

    void Start() {
        Destroy(gameObject, selfDestroyDelay);
    }
}
