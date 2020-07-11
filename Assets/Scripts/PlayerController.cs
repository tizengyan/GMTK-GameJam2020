using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    float speed = 2.0f;
    [SerializeField]
    float leftPosX = -3f, rightPosX = 3, upPosY = 5, downPosY = -5;
    [SerializeField]
    AudioClip jumpSound, landSound, hitSound, dieSound;

    GameObject player;
    bool isStop = true;
    Animator animator;
    AudioSource audioSource;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        isStop = false;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        if (player != null) {
            processInput();       
        }
    }

    void processInput() {
        if (!isStop) {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Debug.Log(moveHorizontal + ", " + moveVertical);
            float x = transform.position.x;
            float y = transform.position.y;
            float distance = speed * Time.deltaTime;
            if (moveHorizontal > 0.5 && x < rightPosX) {
                x += distance;
            }
            else if (moveHorizontal < -0.5 && x > leftPosX) {
                x -= distance;
            }
            if (moveVertical > 0.5 && y < upPosY) {
                y += distance;
            }
            else if (moveVertical < -0.5 && y > downPosY) {
                y -= distance;
            }
            transform.position = new Vector2(x, y);
        }
    }

    public void Stop() {
        isStop = true;
    }

    void Die() {
        animator.SetBool("gameIsOver", true);
        animator.SetTrigger("Dead");
        Debug.Log("Dead");
        audioSource.PlayOneShot(dieSound, 1f);
        //GameManager.GetInstance().StartCoroutine("GameOver");
    }
}
