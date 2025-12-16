using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform boat;
    private Vector3 cameraPosition;
    // Start is called before the first frame update
    void Start()
    {
        cameraPosition.x = boat.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cameraPosition.x = boat.position.x;
        cameraPosition.y = boat.position.y+20;
        cameraPosition.z = boat.position.z-50;

        transform.position = cameraPosition;
    }
}
