using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    [SerializeField] private Transform boat;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = boat.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, boat.position.y, boat.position.z);
    }
}
