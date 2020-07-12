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
    int hitBossScore = 10, hitBulletScore = 1;
    [SerializeField]
    int[] scoreByLevel = { 5, 10, 15};
    [SerializeField]
    TextMeshProUGUI scoreText;
    [SerializeField]
    UnityEvent StartTrigger, overTrigger, winTrigger;
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
    public bool IsGamePaused { set; get; } = false;
    public bool gameStartTrigger { set; get; } = false;

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
        gameStartTrigger = false;
        curScore = 0;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        hudController = hud.GetComponent<HUDController>();
    }
    
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && !IsGameStarted && gameStartTrigger) {
            Debug.Log("GameManager update");
            IsGameStarted = true;
            gameStartTrigger = false;
            StartTrigger.Invoke();
        }
    }

    public void PauseGame()
    {
        audioSource.Pause();
        Debug.Log("Pause Game");
        IsGamePaused = true;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        audioSource.Play();
        Debug.Log("Resume Game");
        IsGamePaused = false;
        Time.timeScale = 1;
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

    public void HitBullet() {
        curScore += hitBulletScore;
        RefreshScoreText();
    }

    void RefreshScoreText() {
        if (scoreText != null) {
            scoreText.text = curScore.ToString();
        }
    }

    public void GameWin() {
        winTrigger.Invoke();
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        gameIsOver = true;
        if (DataManager.HighScore < curScore) {
            DataManager.HighScore = curScore;
        }
        overTrigger.Invoke();
    }

    public void StopGame() {
        Debug.Log("Stop game");
        //StopPlayer();
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

    public void Restart() => SceneManager.LoadScene("MainScene");

    void LevelClear() {
        
    }
}
