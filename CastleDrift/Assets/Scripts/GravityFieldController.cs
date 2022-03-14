using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFieldController : MonoBehaviour
{
    const string TAG_COMPARE = "Player";

    // when in the field, what do we change the gravity to? 
    public float gravity_multiplier;

    //Wings for flying
    private GameObject Wings;

    void Start()
    {
        //set wings false when player starts the game
        Wings = GameObject.Find("Wings");
        Wings.SetActive(false);
    }

        // apply gravity
        private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag(TAG_COMPARE)){
            Debug.Log("enter field");
            Controls_Player player = other.gameObject.GetComponent<Controls_Player>();
            player.AdjustGravity(gravity_multiplier);

            //set wings active when player is in clouds
            Wings.SetActive(true);
        }
    }

    //revert gravity
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag(TAG_COMPARE)){
            Debug.Log("exit field");
            Controls_Player player = other.gameObject.GetComponent<Controls_Player>();
            player.RevertGravity();

            //make wings dissappear when player exits the cloud area
            Wings.SetActive(false);
        }
    }
}
