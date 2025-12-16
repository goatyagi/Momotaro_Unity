using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEyeBat : Eyebat
{
    System.Random r = new System.Random();
    [SerializeField] private bool isChild;
    [SerializeField] private bool isMother;
    [SerializeField] private Transform motherPos;
    [SerializeField] private float period = 1f;

    // Start is called before the first frame update
    protected override void Start()
    {
        upperLimit = transform.position.y + 15;
        lowerLimit = transform.position.y - 30;
        waitAttack = (float)r.NextDouble() * 1.5f;
        verticalSpeed = (float)r.NextDouble() * 2f;
        type = 0;
        rb = GetComponent<Rigidbody>();
        sc = GetComponent<SphereCollider>();
        sc.enabled = false;
    }

    protected  override void FixedUpdate() {
        this.isEncount = transform.parent.gameObject.GetComponent<specialEyebatMoving>().isEncount;

        if (isEncount) {
            if (isChild) {
                sc.enabled = true;
                transform.RotateAround(motherPos.position, Vector3.up, period);
            }
            transform.LookAt(boat.transform);
            transform.position += new Vector3(0f, verticalSpeed, 0f);

            if (transform.position.y >= upperLimit || transform.position.y <= lowerLimit) verticalSpeed = -verticalSpeed;
            if (!isCoolDown) {
                StartCoroutine(attackTimer());
            }
        }
    }
}
