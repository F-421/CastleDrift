using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public int index;
    const string TAG_COMPARE = "Player"; // avoid retyping if I need this again

    // increase our collision count when we hit a checkpoint
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag(TAG_COMPARE)){
            PlayerControlls player = other.gameObject.GetComponent<PlayerControlls>();

            Debug.Log("Checkpoint #" + index + " hit.");

            // make sure it is the right checkpoint
            if(player.checkpointNum == index - 1){
                player.checkpointNum = index;
            }

        }
    }
}
