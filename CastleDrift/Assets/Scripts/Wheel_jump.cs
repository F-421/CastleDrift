using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Represents the wheel object
Resets jump counter when colliding with floor
*/

public class Wheel_jump : MonoBehaviour
{


    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with floor");
            // reset jump count if we hit ground!
        if (collision.collider.tag == "Floor")
        {
            Controls_Player player = gameObject.GetComponentInParent<Controls_Player>();
            player.resetJump();
        }
    }
}
