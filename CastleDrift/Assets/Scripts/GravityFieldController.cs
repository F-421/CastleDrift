using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFieldController : MonoBehaviour
{
    const string PLAYER_TAG_COMPARE = "Player"; // avoid retyping if I need this again
	const string AI_TAG_COMPARE = "AIKart"; // avoid retyping if I need this again


    // when in the field, what do we change the gravity to? 
    public float gravity_multiplier;


    void Start()
    {
        //set wings false when player starts the game
        //Wings = GameObject.Find("Wings");
    }

        // apply gravity
        private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag(PLAYER_TAG_COMPARE) || other.gameObject.CompareTag(AI_TAG_COMPARE)){
            Debug.Log("enter field");
            Controls_Player player = other.gameObject.GetComponent<Controls_Player>();
            player.AdjustGravity(gravity_multiplier);

            //set wings active when player is in clouds
            
            FindObjectOfType<AudioManager>().Play("Flying");
        }
    }

    //revert gravity
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag(PLAYER_TAG_COMPARE) || other.gameObject.CompareTag(AI_TAG_COMPARE)){
            Debug.Log("exit field");
            Controls_Player player = other.gameObject.GetComponent<Controls_Player>();
            player.RevertGravity();

            //make wings dissappear when player exits the cloud area
            FindObjectOfType<AudioManager>().Stop("Flying");
        }
    }
}
