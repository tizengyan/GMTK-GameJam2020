using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour {
    [SerializeField]
    GameObject notePrefabA, notePrefabB;

    public AudioSource bgm;

    string tempoList = "";
    float timePerTempo;
    int listIdx;
    float BPM;

    float timer;
    float gameWinTimer;

    bool isGameStart;
    bool triggerGameWin;

    float initialTime;
    float fakeTime = 0f;
    float trueTime = 0f;

    int counter = 0;
    int trueCounter = 1;
    bool isBGMPlayed;

    void Start() {
        LoadTemopFile();
        BPM = GameManager.GetInstance().GetBPM();
        timePerTempo = 60f / BPM;
        Debug.Log("Start: " + BPM + ", " + timePerTempo);
        listIdx = 0;
        isGameStart = false;
        isBGMPlayed = false;
        triggerGameWin = false;
    }

    void Update()
    {
        if (GameManager.GetInstance().IsGameStarted && !isGameStart) {
            Debug.Log("TempoGenerator update");
            initialTime = Time.fixedTime;
            isGameStart = true;
            timer = Time.fixedTime;
        }
    }

    void FixedUpdate() {
        if (isGameStart && Time.fixedTime >= initialTime + timePerTempo * trueCounter)
        {
            if (!isBGMPlayed)
            {
                PlayBGM();
            }

            GenerateTempo();

            if (counter >= 4)
            {
                trueTime = timePerTempo * counter;
                fakeTime = Time.fixedTime - timer;
                Debug.Log("True Time is " + trueTime);
                Debug.Log("Fake Time is " + fakeTime);

                counter = 0;
                fakeTime = 0f;
                timer = Time.fixedTime;
            }
        }

        if (triggerGameWin && Time.fixedTime - gameWinTimer >= 5f) 
        {
            GameManager.GetInstance().GameWin();
        }
    }

    public void PlayBGM()
    {
        bgm.Play();
        isBGMPlayed = true;
    }

    public void PauseBGM()
    {
        bgm.Pause();
    }

    public void BeginGenerate() {
        StartCoroutine("GenerateTempo");
    }

    void GenerateTempo() {
        counter++;
        trueCounter++;

        if (listIdx < tempoList.Length)
        {
            if (tempoList[listIdx] == 'A')
            {
                Instantiate(notePrefabA, transform.position, notePrefabA.transform.rotation);
            }
            else if (tempoList[listIdx] == 'B')
            {
                Instantiate(notePrefabB, transform.position, notePrefabB.transform.rotation);
            }
            listIdx++;
        }
        else if (!triggerGameWin)
        {
            triggerGameWin = true;
            gameWinTimer = Time.fixedTime;
        }
    }

    void LoadTemopFile() {
        //string path = "Assets/Resources/tempo.txt";
        //StreamReader sr = new StreamReader(path);
        //tempoList = sr.ReadToEnd();
        TextAsset mytxtData = Resources.Load<TextAsset>("tempo");
        tempoList = mytxtData.text;
        Debug.Log("LoadTemopFile " + tempoList);
    }
}
