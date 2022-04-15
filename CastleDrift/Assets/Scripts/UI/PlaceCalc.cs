using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* This script will calculate place in relation to other cars on track*/

public class PlaceCalc : MonoBehaviour
{
	public GameObject player_car;
	[SerializeField] private List<GameObject> cpu_cars;
	
	[SerializeField] TextMeshProUGUI placeText;
	
    // Start is called before the first frame update
    void Start()
    {
        placeText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        placeText.text = "#" + (CalcPlace() + 1);
    }
	
	public int CalcPlace(){
		
		int player_place = 0;
		Controls_Player player = player_car.GetComponent<Controls_Player>();
		int player_checkpoint = player.checkpointNum;
        int player_lap = player.lapNum;
		
		for(int i = 0; i < cpu_cars.Count; i++){
			Controls_Player car = cpu_cars[i].GetComponent<Controls_Player>();
			
			// player is behind if on different lap
			if(car.lapNum > player_lap){
				player_place++;
			}
			else if (car.lapNum == player_lap){
				
				//player is behind if on different checkpoint
				if(car.checkpointNum > player_checkpoint){
					player_place++;
				}
				else if (car.checkpointNum == player_checkpoint){
					
					//player is behind car physically
					Vector3 dirToMovePosition = (car.transform.position - player.transform.position).normalized;
					float dot = Vector3.Dot(car.transform.forward, dirToMovePosition);
					if(dot > 0){
						player_place++;
					}
				}
			}

		}
		
		return player_place;
	}
}
