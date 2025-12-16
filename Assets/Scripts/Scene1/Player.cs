using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float speed = 10;
    [SerializeField] private float dashSpeed = 15;
    [SerializeField] private float jumpPower = 2500;
    [SerializeField] private float gravity = 1f;
    [SerializeField] private GameObject cam;
    float x = 0, z = 0;
    Rigidbody rb;
    Animator anim;
    
    Vector3 moving;
    Vector3 localMoving;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }


    void FixedUpdate() {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, cam.transform.eulerAngles.y, 0f);

        //localMoving = localMoving * Time.fixedDeltaTime;
        localMoving += new Vector3(0f, -gravity, 0f);
        rb.AddForce(localMoving);
    }

    public void move() {
        anim.SetBool("isJumping", false);
        
        if (Input.GetKey(KeyCode.Space)){
            anim.SetBool("isJumping", true);
            rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
        }
        
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        if (x == 0 && z == 0) {
            localMoving = Vector3.zero;
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
        } else {
            moving = new Vector3(x, 0f, z);

            if (Input.GetKey(KeyCode.LeftShift)) {
                moving = moving.normalized;
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", true);
                localMoving = transform.TransformDirection(moving) * dashSpeed;
            }
            else {
                moving = moving.normalized;
                anim.SetBool("isRunning", false);
                anim.SetBool("isWalking", true);
                localMoving = transform.TransformDirection(moving) * speed;
            }
            transform.forward = localMoving;
        }
        
    }
}
