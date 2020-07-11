using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {
    [SerializeField]
    GameObject mainMenu;
    [SerializeField]
    Text score;
    [SerializeField]
    Spawner[] spawnPoints;
    [SerializeField]
    float gameStartDelay = 2f;

    bool gameIsOver = false;
    int curScore = 0;

    public float GameStartDelay() => gameStartDelay;
    public int GetCurScore() => curScore;

    public int GetHP() {
        return 1;
    }

    void Awake() {

    }

    void Start() {
        //gameIsOver = false;
        curScore = 0;
    }
    
    void Update() {
        
    }

    IEnumerator GameOver()
    {
        Debug.Log("Game Over");
        DataManager.TotalScore += curScore;
        StopAllObstacles();
        StopPlayer();
        gameIsOver = true;

        yield return new WaitForSeconds(3.2f);
    }

    public void StopGame() {
        Debug.Log("Stop game");
        StopAllObstacles();
        StopPlayer();
        gameIsOver = true;
    }

    void StopPlayer() {
        if (GameObject.FindWithTag("Player") == null) {
            Debug.LogError("Player not found");
            return;
        }
        PlayerController pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        if (pc) {
            pc.Stop();
        }
    }

    void StopAllObstacles() {
        // stop spawning
        foreach (var sp in spawnPoints) {
            sp.setGameIsOver(true);
        }
    }

    public void Restart() => SceneManager.LoadScene("MainScene");

    void levelClear() {
        
    }
}
