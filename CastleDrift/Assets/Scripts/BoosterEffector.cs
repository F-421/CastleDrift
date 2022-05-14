using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterEffector : MonoBehaviour
{
    public float boost_addition; //how much velocity do we add when we boost?
    public float boost_time; 
    const string PLAYER_TAG_COMPARE = "Player"; // avoid retyping if I need this again
	const string AI_TAG_COMPARE = "AIKart"; // avoid retyping if I need this again  

    // give the player a speed boost when hit
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag(PLAYER_TAG_COMPARE)){
            Debug.Log("Booster hit");

            Controls_Player player = other.gameObject.GetComponentInParent<Controls_Player>();

            player.TurnBoostOn(boost_time, boost_addition);
        }
        if(other.gameObject.CompareTag(AI_TAG_COMPARE)){
            Debug.Log("Booster hit");

            Controls_Player player = other.gameObject.GetComponent<Controls_Player>();

            player.TurnBoostOn(boost_time, boost_addition);
        }
    }
}
