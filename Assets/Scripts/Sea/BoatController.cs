using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public static BoatController Instance { get; private set; }
    [SerializeField] private float speed = 1f;
    [SerializeField] private float horizontalSpeed = 5f;
    [SerializeField] private int life = 3;
    [SerializeField] private float delayTime = 1f;
    [SerializeField] private ParticleSystem explore;
    [SerializeField] private HPManager hpManager;
    public int score = 0;
    GameManager gameManager;
    MeshCollider mc;

    Animator anim;
    Vector3 moving;

    float x = 0;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        anim = GetComponent<Animator>();
        gameManager = GetComponent<GameManager>();
        mc = GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(0f, 0f, speed);
        x = Input.GetAxisRaw("Horizontal");

        if ( x == 0 ) moving = Vector3.zero;
        else {
            moving = new Vector3(x, 0f, 0f);
            moving = moving.normalized * horizontalSpeed * Time.fixedDeltaTime;
        }
        if (transform.position.x + moving.x <= 60 && transform.position.x + moving.x >= -60) {
            transform.position += moving;
        }
    }

    void OnTriggerEnter(Collider collision) {
        // layer 7 is damager
        if ( collision.gameObject.layer == 7 
        || collision.gameObject.layer == 8
        || collision.gameObject.layer == 6) {
            life--;
            hpManager.decreaseHP();
            
            if ( life > 0 ) StartCoroutine(DelayCoroutine());
            else {
                explore.Play();
                anim.SetBool("isDead", true);
                speed = 0f;
                horizontalSpeed = 0f;
                mc.enabled = false;
                gameManager.EndGame();
            }
        } else {

        }
    }

    // いわゆる被ダメージ後の無敵時間
    private IEnumerator DelayCoroutine() {
        anim.SetBool("isDamaged", true);
        mc.enabled = false;
        yield return new WaitForSeconds(delayTime);
        anim.SetBool("isDamaged", false);
        mc.enabled = true;
    }

    public float GetSpeed() {
        return speed;
    }
}
