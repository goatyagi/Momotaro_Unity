using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyebat : MonoBehaviour
{
    [SerializeField] protected float distance = 250f;
    [SerializeField] protected Transform boat;
    [SerializeField] protected Transform attackPos;
    [SerializeField] protected Transform effectPos;
    [SerializeField] protected ParticleSystem death;
    [SerializeField] protected float attackInterval = 3f;
    [SerializeField] protected float attackPower = 1000;
    [SerializeField] protected float verticalSpeed = 1f;
    [SerializeField] protected GameObject magicBall;
    [SerializeField] protected float eyebatSpeed = 5f;
    [SerializeField] protected Material[] materialSet = new Material[2];
    [SerializeField] protected SkinnedMeshRenderer eyeSkin;
    protected float waitAttack;
    protected bool isEncount = false;
    protected bool isCoolDown = false;
    bool isAttackingWith02 = false;

    protected float upperLimit = 57.9f + 30;
    protected float lowerLimit = 57.9f - 30;
    protected int posY;
    
    // type変数は、モンスターの攻撃パターンを決定するための変数であり、概要は以下の通り。
    // 0: マジックボール攻撃, 1: 体当たり攻撃
    [SerializeField] protected int type = 0; 
    System.Random r = new System.Random();
    protected Rigidbody rb;
    protected SphereCollider sc;

    protected virtual void Start() {
        type = r.Next(2);
        posY = r.Next(30, 85);
        distance = (float)r.Next(150, 301);
        int var = r.Next(1, 4);
        verticalSpeed = (float)var * 0.5f;

        waitAttack = (float)r.NextDouble() * 1.5f;

        transform.position = new Vector3(transform.position.x, (float)posY, transform.position.z);

        if (type == 1) {
            eyeSkin.material = materialSet[1];
            verticalSpeed = -verticalSpeed;
            waitAttack = waitAttack + 1;
        }

        rb = GetComponent<Rigidbody>();
        sc = GetComponent<SphereCollider>();
        sc.enabled = false;
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        if ( !isAttackingWith02 ) {
            transform.position += new Vector3(0f, verticalSpeed, 0f);
            if (transform.position.y >= upperLimit || transform.position.y <= lowerLimit) verticalSpeed = -verticalSpeed;

            if ( transform.position.z - boat.position.z <= distance) isEncount = true;
            if ( isEncount ) {
                sc.enabled = true;
                transform.LookAt(boat.transform);
                transform.position = new Vector3(transform.position.x, transform.position.y, boat.position.z + distance);

                if ( !isCoolDown ) {
                    StartCoroutine(attackTimer());
                }
            }
        } else {
            Vector3 dirToBoat = GetDirection(boat.transform, transform);
            dirToBoat.y -= 20;
            dirToBoat = dirToBoat.normalized;
            transform.position += dirToBoat * eyebatSpeed;
        }
    }

    protected void Attack01() {
        GameObject ball = Instantiate(magicBall, attackPos.position , magicBall.transform.rotation);
        Vector3 toBoat = GetDirection(boat.transform, attackPos);

        Rigidbody rb_ball = ball.GetComponent<Rigidbody>();
        rb_ball.AddForce(toBoat.normalized * attackPower, ForceMode.Impulse);
    }

    protected void Attack02() {
        isAttackingWith02 = true;
        Debug.Log("02");
    }

    protected IEnumerator attackTimer() {
        isCoolDown = true;

        yield return new WaitForSeconds(waitAttack);
        if ( type == 0 ) Attack01();
        else if ( type == 1 ) Attack02();
        waitAttack = attackInterval;

        isCoolDown = false;
    }
    
    // AからBの方向をVector3型で返す関数
    protected Vector3 GetDirection(Transform A, Transform B) {
        Vector3 direction = Vector3.zero;

        direction.x = A.position.x - B.position.x;
        direction.y = A.position.y - B.position.y;
        direction.z = A.position.z - B.position.z;

        return direction;
    }

    protected void OnTriggerEnter(Collider collision) {
        if ( collision.gameObject.layer == 9) {
            ParticleSystem effect = Instantiate(death, effectPos.position, death.transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
