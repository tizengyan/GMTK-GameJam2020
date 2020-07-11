using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    float speed = 25.0f;
    [SerializeField]
    float leftPosX = -3f, rightPosX = 3, upPosY = 5, downPosY = -5;
    [SerializeField]
    AudioClip jumpSound, landSound, hitSound, dieSound;
    [SerializeField]
    GameObject firePoint;
    [SerializeField]
    GameObject[] lasers;
    [SerializeField]
    GameObject[] sectors;

    Vector2 moveDirection;
    GameObject player;
    bool isStop = true;
    Animator animator;
    AudioSource audioSource;
    Rigidbody2D rb;
    public int HitPoint { set; get; } = 5;
    // Tempo: 0 Miss, 1 Good, 2 Cool, 3 Great
    public int Tempo { set; get; } = 1;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        isStop = false;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (player != null) {
            ProcessInput();       
        }
    }

    void ProcessInput() {
        if (!isStop) {
            float x = transform.position.x;
            float y = transform.position.y;
            float distance = speed * Time.deltaTime;
            if (Input.GetKey(KeyCode.RightArrow) && x < rightPosX) {
                x += distance;
            }
            if (Input.GetKey(KeyCode.LeftArrow) && x > leftPosX) {
                x -= distance;
            }
            if (Input.GetKey(KeyCode.UpArrow) && y < upPosY) {
                y += distance;
            }
            if (Input.GetKey(KeyCode.DownArrow) && y > downPosY) {
                y -= distance;
            }
            transform.position = new Vector2(x, y);

            if (Input.GetKeyDown(KeyCode.A) && Tempo != 0) {
                LaserAttack();
            }
            // 二选一
            else if (Input.GetKeyDown(KeyCode.B) && Tempo != 0) {
                SectorAttack();
            }
        }
    }

    void LaserAttack() {
        GameObject ins = Instantiate(lasers[Tempo], firePoint.transform.position, firePoint.transform.rotation);
        ins.transform.position = new Vector2(transform.position.x, transform.position.y + 3.5f);
        ins.transform.parent = gameObject.transform;
    }

    void SectorAttack() {
        GameObject ins = Instantiate(sectors[Tempo], firePoint.transform.position, sectors[Tempo].transform.rotation);
        ins.transform.parent = gameObject.transform;
    }

    public void Stop() {
        isStop = true;
    }

    void OnHit() {
        HitPoint--;
        animator.SetTrigger("Hit");
        if (HitPoint <= 0) {
            Die();
        }
    }

    void Die() {
        animator.SetBool("gameIsOver", true);
        animator.SetTrigger("Dead");
        Debug.Log("Dead");
        audioSource.PlayOneShot(dieSound, 1f);
        //GameManager.GetInstance().StartCoroutine("GameOver");
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            OnHit();
        }
    }
}
