using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMoving : MonoBehaviour
{
    float speed;

    void Start() {
        speed = BoatController.Instance.GetSpeed();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(0f, 0f, speed);
    }
}
