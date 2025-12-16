using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform obj;
    [SerializeField] private GameObject player;
    [SerializeField] private float sensRotate = 5.0f;

    Camera cam;

    void Start() {
        cam = GetComponent<Camera>();
        transform.rotation = player.transform.rotation;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    { 
        transform.position = obj.position;

        float rotateX = Input.GetAxis("Mouse X") * sensRotate;
        float rotateY = Input.GetAxis("Mouse Y") * sensRotate;

        if (cam.transform.eulerAngles.x - rotateY > 310 || cam.transform.eulerAngles.x - rotateY< 50 ) {
            cam.transform.Rotate(-rotateY, 0.0f, 0.0f);
        }

        cam.transform.Rotate(0.0f, rotateX, 0.0f);
        cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x, cam.transform.eulerAngles.y, 0f);
    }
}
