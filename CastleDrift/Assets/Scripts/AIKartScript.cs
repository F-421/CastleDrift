using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AIKartScript : MonoBehaviour
{
	public float forwardInput; // setting how much we move forward
	private float DRIFT_ANGLE = 80;

	private Controls_Player car_driver;
	[SerializeField] private List<Transform> waypoints; //list of places to go on route
	private int waypoint_i;
	
	private Vector3 targetPosition;
	private bool is_drifting;

	const string TAG_COMPARE = "AIWaypoint"; // how we identify that we make it to a waypoint
	
	private void Awake(){
		car_driver = GetComponent<Controls_Player>();
		waypoint_i = 0;
		is_drifting = false;
		//forwardInput = 1;
	}


    // Update is called once per frame
    void Update()
    {
		SetTargetPosition(waypoints[waypoint_i].position);
		
		Vector3 dirToMovePosition = (targetPosition - transform.position).normalized;
		float dot = Vector3.Dot(transform.forward, dirToMovePosition);
		
		// are we moving towards or away form our waypoint?
		float forwardSpeed = forwardInput;
		if (dot < 0){
			forwardSpeed = -forwardInput;
		}

		// turn car towards waypoint
		float angleToMove = Vector3.SignedAngle(transform.forward, dirToMovePosition, Vector3.up);
		float turnSpeed = 0f;
		
		//Debug.Log("Move angle " + angleToMove + " " + is_drifting);
		
		if(angleToMove > 0){
			turnSpeed = 1f;
			
			// let's try to have the AIkart drift
			/*if(angleToMove > DRIFT_ANGLE){
				car_driver.StartDrift();
				is_drifting = true;
			}
			else if(is_drifting){
				car_driver.ReleaseDrift();
				is_drifting = false;
			}*/
		}
		else if(angleToMove < 0){
			turnSpeed = -1f;
			
			if(angleToMove < -1 * DRIFT_ANGLE){
				car_driver.StartDrift();
				is_drifting = true;
			}
			else if(is_drifting){
				car_driver.ReleaseDrift();
				is_drifting = false;
			}
		}

		car_driver.SetMovement(forwardSpeed, turnSpeed);
		//car_driver.SetMovement(1, 0);
	}
	
	public void SetTargetPosition(Vector3 targetPosition){
		this.targetPosition = targetPosition;
	}

	/*collided with another object*/
	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag(TAG_COMPARE))
        {
			Debug.Log("AI Kart collided with a waypoint " + waypoint_i);

			//is it our current waypoint? 
			GameObject collidedwaypoint = other.gameObject;
            if ((waypoints[waypoint_i] is not null) && (collidedwaypoint.transform != waypoints[waypoint_i]))
            {
                return;
            }

			Debug.Log("We hit the correct waypoint" + waypoint_i);

			waypoint_i++;

			// hit end of waypoint list: back to beginning! (this should cover a lap of track)
			if(waypoint_i == waypoints.Count){
				waypoint_i = 0;
			}

		}
	}
}