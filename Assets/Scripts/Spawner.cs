using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [SerializeField]
    private GameObject prefab;
    //[SerializeField]
    //private float intervalMin = 1f, intervalMax = 3f;

    float startDelay = 2f;
    public float spawnInterval = 0.8f;
    bool gameIsOver = false;

    public void setGameIsOver(bool isOver) {
        gameIsOver = isOver;
    }

    void Start() {
        GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        startDelay = gm.GameStartDelay();
        Invoke("BeginSpawn", startDelay);
    }

    void BeginSpawn() {
        StartCoroutine("Spawning");
    }

    IEnumerator Spawning() {
        while (!gameIsOver) {
            //spawnInterval = Random.Range(intervalMin, intervalMax);
            Instantiate(prefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    
    void Update() {

    }
}
