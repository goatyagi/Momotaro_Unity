using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    public void decreaseHP() {
        GameObject child = transform.GetChild(0).gameObject;
        Destroy(child);
    }
}
