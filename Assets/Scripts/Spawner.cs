using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [SerializeField]
    GameObject prefab;
    [SerializeField]
    bool isRandomInterval = false;
    [SerializeField]
    float intervalMin = 1f, intervalMax = 3f;

    float startDelay = 2f;
    public float spawnInterval = 0.8f;
    bool gameIsOver = false;
    PositionRandomizer pr;

    public void setGameIsOver(bool isOver) {
        gameIsOver = isOver;
    }

    void Start() {
        GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        startDelay = gm.GameStartDelay();
        Invoke("BeginSpawn", startDelay);

        pr = GetComponent<PositionRandomizer>();
    }

    void BeginSpawn() {
        StartCoroutine("Spawning");
    }

    IEnumerator Spawning() {
        while (!gameIsOver) {
            // 也可以用pr脚本自己去设置delay，但有可能不同步，这里查可以确保移动了位置
            if (pr != null) {
                pr.GotoNewPosition();
            }
            if (isRandomInterval) {
                spawnInterval = Random.Range(intervalMin, intervalMax);
            }
            //spawnInterval = Random.Range(intervalMin, intervalMax);
            Instantiate(prefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    
    void Update() {

    }
}
