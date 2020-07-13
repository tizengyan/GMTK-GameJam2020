using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {
    [SerializeField]
    Sprite[] hpSprites;
    [SerializeField]
    GameObject hpPanel;
    [SerializeField]
    GameObject[] tipPanels;

    PlayerController pc;

    void Start() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();
    }

    public void RefreshHP() {
        int curHp = pc.GetHp();
        if (curHp < hpSprites.Length) {
            hpPanel.GetComponent<Image>().sprite = hpSprites[curHp];
        }
    }

    public void ShowHitTip(int hitLevel) {
        Debug.Log("ShowHitTip");
        if (hitLevel < tipPanels.Length) {
            for(int i = 0; i < tipPanels.Length; i++) {
                tipPanels[i].SetActive(i == hitLevel);
            }
            Animator animator = tipPanels[hitLevel].GetComponent<Animator>();
            animator.SetTrigger("show");
        }
    }
}
