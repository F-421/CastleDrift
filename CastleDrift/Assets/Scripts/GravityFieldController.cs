using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFieldController : MonoBehaviour
{
    const string TAG_COMPARE = "Player";

    // when in the field, what do we change the gravity to? 
    public float gravity_multiplier;

    // apply gravity
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag(TAG_COMPARE)){
            Debug.Log("enter field");
            Controls_Player player = other.gameObject.GetComponent<Controls_Player>();
            player.AdjustGravity(gravity_multiplier);
        }
    }

    //revert gravity
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag(TAG_COMPARE)){
            Debug.Log("exit field");
            Controls_Player player = other.gameObject.GetComponent<Controls_Player>();
            player.RevertGravity();
        }
    }
}
