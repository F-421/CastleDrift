using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// this is to controll the game (and so we eventually have a stopping point)
public class LapController : MonoBehaviour
{
    //public PlaceCalcForWinScreen place;
    public List<CheckpointController> checkpoints;
    public int totalLaps;
    const string PLAYER_TAG_COMPARE = "Player"; // avoid retyping if I need this again
	const string AI_TAG_COMPARE = "AIKart"; // avoid retyping if I need this again

    public GameObject WinScreen;
    public GameObject SecondPlace;
    public GameObject ThirdPlace; //I couldn't get activation by tag working

    private PlaceCalc Place;
    public GameObject StartFinishLine;
   

    //update the text displaying lap number
    [SerializeField] TextMeshProUGUI lapText;

    void Start(){
        lapText.text = "1/" + totalLaps;
        Place = StartFinishLine.GetComponent<PlaceCalc>();
    }

    // do we increase lap? endgame?
    private void OnTriggerEnter(Collider other) {

        // player car lap
        if(other.gameObject.CompareTag(PLAYER_TAG_COMPARE)){
            Controls_Player player = other.gameObject.GetComponent<Controls_Player>();
			Debug.Log(player.lapNum);

            if(player.checkpointNum == checkpoints.Count){
                
                player.checkpointNum = 0;
                player.lapNum++;

                //update the respawn
                player.UpdateRespawn(transform.position, transform.rotation);

                //now we need to adjust the lap text
                lapText.text = player.lapNum + "/" + totalLaps;
                Debug.Log("Lap " + player.lapNum + "/" + totalLaps);
 
                // do we end game?
                if(player.lapNum > totalLaps){
                    Debug.Log("Game End here");
                    Debug.Log(Place.place);

                    if (Place.place == 0)
                    {
                        // show end game screen (and freeze time)
                        Time.timeScale = 0;
                        WinScreen.gameObject.SetActive(true);
                        lapText.gameObject.SetActive(false);
                    }

                    else if (Place.place == 1)
                    {
                        // show end game screen (and freeze time)
                        Time.timeScale = 0;
                        SecondPlace.gameObject.SetActive(true);
                        lapText.gameObject.SetActive(false);
                    }

                    else if (Place.place == 2)
                    {
                        // show end game screen (and freeze time)
                        Time.timeScale = 0;
                        ThirdPlace.gameObject.SetActive(true);
                        lapText.gameObject.SetActive(false);
                    }

                }
            }
        }

        // opponent car lap
        else if(other.gameObject.CompareTag(AI_TAG_COMPARE)){
            Controls_Player player = other.gameObject.GetComponent<Controls_Player>();
			Debug.Log(player.lapNum);
			
            if(player.checkpointNum == checkpoints.Count){
                
                player.checkpointNum = 0;
                player.lapNum++;

                //update the respawn
                player.UpdateRespawn(transform.position, transform.rotation);
            }
        }
    }
}
