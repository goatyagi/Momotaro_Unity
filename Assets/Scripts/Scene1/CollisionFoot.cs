using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionFoot : MonoBehaviour
{
    [SerializeField] private Player player;
    void OnTriggerStay(Collider collision) {
        player.move();
    }
}
