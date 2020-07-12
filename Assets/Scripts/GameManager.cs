using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {
    [SerializeField]
    GameObject mainMenu, hud;
    [SerializeField]
    float BPM = 120f;
    [SerializeField]
    int hitBossScore = 10;
    [SerializeField]
    int[] scoreByLevel = { 5, 10, 15};
    [SerializeField]
    TextMeshProUGUI scoreText;
    [SerializeField]
    UnityEvent StartTrigger;
    [SerializeField]
    KeyCode laserAttackKey;

    AudioSource audioSource;
    bool gameIsOver;
    int curScore;
    static GameManager instance;
    PlayerController pc;
    HUDController hudController;

    public int GetCurScore() => curScore;
    public float GetBPM() => BPM;
    public bool IsGameStarted { set; get; } = false;

    public int GetHP() {
        return 1;
    }

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    void Start() {
        gameIsOver = false;
        curScore = 0;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        hudController = hud.GetComponent<HUDController>();
    }
    
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && !IsGameStarted) {
            Debug.Log("GameManager update");
            IsGameStarted = true;
            StartTrigger.Invoke();
        }
    }

    public static GameManager GetInstance() {
        return instance;
    }

    public void PlayBGM() {
        audioSource.Play();
    }

    public void HitTempo(int hitLevel, KeyCode hitKey) {
        if (hitLevel < scoreByLevel.Length) {
            curScore += scoreByLevel[hitLevel];
            RefreshScoreText();
            hudController.ShowHitTip(hitLevel);
        }
        if (hitKey == laserAttackKey) {
            pc.LaserAttack(hitLevel);
        }
        else {
            pc.SectorAttack(hitLevel);
        }
    }

    public void HitBoss() {
        curScore += hitBossScore;
        RefreshScoreText();
    }

    void RefreshScoreText() {
        if (scoreText != null) {
            scoreText.text = curScore.ToString();
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        StopAllObstacles();
        StopPlayer();
        gameIsOver = true;
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
        
    }

    public void Restart() => SceneManager.LoadScene("MainScene");

    void LevelClear() {
        
    }
}
