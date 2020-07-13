using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TempoGenerator : MonoBehaviour {
    [SerializeField]
    GameObject notePrefabA, notePrefabB;

    string tempoList;
    float timePerTempo;
    int listIdx;
    float BPM;

    float timer;
    float gameWinTimer;

    bool isGameStart;
    bool triggerGameWin;

    void Start() {
        LoadTemopFile();
        BPM = GameManager.GetInstance().GetBPM();
        timePerTempo = 60f / BPM;
        Debug.Log("Start: " + BPM + ", " + timePerTempo);
        listIdx = 0;
        isGameStart = false;
        triggerGameWin = false;
    }

    void Update()
    {
        if (GameManager.GetInstance().gameStartTrigger && !isGameStart) {
            Debug.Log("TempoGenerator update");
            timer = Time.fixedTime;
            isGameStart = true;
        }
    }

    void FixedUpdate() {
        if (isGameStart && Time.fixedTime - timer >= timePerTempo)
        {
            GenerateTempo();
            timer = Time.fixedTime;
        }

        if (triggerGameWin && Time.fixedTime - gameWinTimer >= 5f) 
        {
            GameManager.GetInstance().GameWin();
        }
    }



    public void BeginGenerate() {
        StartCoroutine("GenerateTempo");
    }

    void GenerateTempo() {
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
        TextAsset mytxtData = (TextAsset)Resources.Load("tempo");
        string tempoList = mytxtData.text;
        Debug.Log(tempoList);
    }
}
