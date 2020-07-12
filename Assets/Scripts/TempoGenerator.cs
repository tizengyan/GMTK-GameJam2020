using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TempoGenerator : MonoBehaviour {
    [SerializeField]
    GameObject notePrefabA, notePrefabB;

    string tempoList;
    float tempoInterval;
    int listIdx;
    float BPM;

    void Start() {
        LoadTemopFile();
        BPM = GameManager.GetInstance().GetBPM();
        tempoInterval = 60f / BPM;
        Debug.Log("Start: " + BPM + ", " + tempoInterval);
        listIdx = 0;
    }

    void Update() {

    }

    public void BeginGenerate() {
        StartCoroutine("GenerateTempo");
    }

    IEnumerator GenerateTempo() {
        while (listIdx < tempoList.Length) {
            if (tempoList[listIdx] == 'A') {
                Instantiate(notePrefabA, transform.position, notePrefabA.transform.rotation);
            }
            else if (tempoList[listIdx] == 'B') {
                Instantiate(notePrefabB, transform.position, notePrefabB.transform.rotation);
            }
            listIdx++;
            yield return new WaitForSeconds(tempoInterval);
        }
        GameManager.GetInstance().GameWin();
    }

    void LoadTemopFile() {
        string path = "Assets/Resources/tempo.txt";
        StreamReader sr = new StreamReader(path);
        tempoList = sr.ReadToEnd();
        Debug.Log(tempoList);
    }
}
