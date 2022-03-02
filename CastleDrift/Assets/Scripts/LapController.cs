using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// this is to controll the game (and so we eventually have a stopping point)
public class LapController : MonoBehaviour
{

    public List<CheckpointController> checkpoints;
    public int totalLaps;
    const string TAG_COMPARE = "Player";  

    public GameObject gameOver; //I couldn't get activation by tag working

    //update the text displaying lap number
    [SerializeField] TextMeshProUGUI lapText;

    void Start(){
        lapText.text = "1/" + totalLaps;
    }

    // do we increase lap? endgame?
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag(TAG_COMPARE)){
            PlayerControlls player = other.gameObject.GetComponent<PlayerControlls>();

            if(player.checkpointNum == checkpoints.Count){
                
                player.checkpointNum = 0;
                player.lapNum++;

                //update the respawn
                player.UpdateRespawn(transform.position);

                //now we need to adjust the lap text
                lapText.text = player.lapNum + "/" + totalLaps;
                Debug.Log("Lap " + player.lapNum + "/" + totalLaps);
 
                // do we end game?
                if(player.lapNum > totalLaps){
                    Debug.Log("Game End here");

                    // show end game screen (and freeze time)
                    Time.timeScale = 0;
                    gameOver.gameObject.SetActive(true);
                    lapText.gameObject.SetActive(false);
                }
            }
        }
    }
}
