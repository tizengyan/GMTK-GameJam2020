﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    float speed = 25.0f;
    [SerializeField]
    float leftPosX = -3f, rightPosX = 3, topPosY = 5, bottomPosY = -5;
    [SerializeField]
    AudioClip hitSound, dieSound;
    [SerializeField]
    GameObject firePoint;
    [SerializeField]
    GameObject[] lasers;
    [SerializeField]
    GameObject[] sectors;
    [SerializeField]
    UnityEvent RefreshHP;

    bool isStop = true;
    Animator animator;
    AudioSource audioSource;
    Rigidbody2D rb;
    bool isInvincible = false;

    public int hitPoint = 6;
    // Tempo: 0 Miss, 1 Good, 2 Cool, 3 Great
    public int Tempo { set; get; } = 1;

    public int GetHp() => hitPoint;

    void Start() {
        isStop = false;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        ProcessInput();    
    }

    void ProcessInput() {
        if (GameManager.GetInstance().IsGameStarted && !isStop) {
            float x = transform.position.x;
            float y = transform.position.y;
            float distance = speed * Time.deltaTime;
            if (Input.GetKey(KeyCode.RightArrow) && x < rightPosX) {
                x += distance;
            }
            if (Input.GetKey(KeyCode.LeftArrow) && x > leftPosX) {
                x -= distance;
            }
            if (Input.GetKey(KeyCode.UpArrow) && y < topPosY) {
                y += distance;
            }
            if (Input.GetKey(KeyCode.DownArrow) && y > bottomPosY) {
                y -= distance;
            }
            transform.position = new Vector2(x, y);

            //if (Input.GetKeyDown(KeyCode.A) && Tempo != 0) {
            //    LaserAttack();
            //}
            //// 二选一
            //else if (Input.GetKeyDown(KeyCode.B) && Tempo != 0) {
            //    SectorAttack();
            //}
        }
    }

    public void LaserAttack(int hitLevel) {
        GameObject ins = Instantiate(lasers[hitLevel], firePoint.transform.position, firePoint.transform.rotation);
        //ins.transform.position = new Vector2(transform.position.x, transform.position.y + 3.5f);
        //ins.transform.parent = gameObject.transform;
    }

    public void SectorAttack(int hitLevel) {
        GameObject ins = Instantiate(sectors[hitLevel], firePoint.transform.position, sectors[hitLevel].transform.rotation);
        ins.transform.parent = gameObject.transform;
    }

    public void Stop() {
        isStop = true;
    }

    void OnHit() {
        if (!isInvincible) {
            isInvincible = true;
            Invoke("cancelInvincible", 1);
            hitPoint--;
            RefreshHP.Invoke();
            animator.SetTrigger("Hit");
            if (hitPoint <= 0) {
                Die();
            }
            //audioSource.PlayOneShot(hitSound, 1f);
            Debug.Log("Hit, hp = " + hitPoint);
        }
    }

    void cancelInvincible() {
        isInvincible = false;
    }

    void Die() {
        animator.SetBool("gameIsOver", true);
        animator.SetTrigger("Dead");
        Debug.Log("Dead");
        //Destroy(gameObject);
        gameObject.SetActive(false);
        audioSource.PlayOneShot(dieSound, 1f);
        GameManager.GetInstance().GameOver();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Enemy" || collision.tag == "Bullet") {
            OnHit();
        }
    }
}
