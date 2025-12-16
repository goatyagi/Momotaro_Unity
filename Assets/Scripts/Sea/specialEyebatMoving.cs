using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class specialEyebatMoving : MonoBehaviour {
    [SerializeField] private float distance = 250f;
    [SerializeField] private Transform boat;
    [SerializeField] private GameObject[] children = new GameObject[12];
    [SerializeField] private GameObject mother;

    public bool isEncount = false;

    // Update is called once per frame
    void FixedUpdate() {
        if (transform.position.z - boat.position.z <= distance) isEncount = true;
        if (isEncount) {
            transform.position = new Vector3(transform.position.x, transform.position.y, boat.position.z + distance);
            if (transform.childCount == 1) {
                mother.transform.Find("shield").gameObject.SetActive(false);
                mother.GetComponent<SphereCollider>().enabled = true;
            }
        }
    }

    
}
