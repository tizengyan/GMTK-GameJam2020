using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private Text score;
    [SerializeField]
    private Spawner[] spawnPoints;
    [SerializeField]
    private float gameStartDelay = 2f;
    [SerializeField]
    private int scoreDemand = 10, levelLimit = 10;

    [SerializeField]
    private float speedBase = 1f, speedRiseRatio = 0.1f;

    [SerializeField]
    private GameObject endHouse;

    static GameManager instance = null;
    bool gameIsOver = false;
    int curScore = 0;
    int curLevel = 1;
    float nextAddScoreTime;

    public float GameStartDelay() => gameStartDelay;
    public int GetCurScore() => curScore;
    public float GetSpeedRatio() => speedRiseRatio;
    public float GetSpeedBase() => speedBase;

    public int GetHP() {
        return 1;
    }

    void Awake() {
        Debug.Log("Awake");
        if (null == instance) {
            instance = this;
        }
        else if (instance != this) {
            Debug.LogWarning("Destroy duplicate gm");
            Destroy(gameObject);
        }
        curLevel = DataManager.CurLevel;
        speedRiseRatio *= curLevel - 1;
    }

    void Start() {
        Debug.Log("Start");
        //gameIsOver = false;
        curScore = 0;
        nextAddScoreTime = Time.time + 1;
    }
    
    void Update() {
        
    }

    public static GameManager GetInstance() {
        return instance;
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
