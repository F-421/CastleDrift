using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AIKartScript : MonoBehaviour
{
	private Controls_Player car_driver;
	[SerializeField] private List<Transform> waypoints; //list of places to go on route
	private int waypoint_i;
	
	private Vector3 targetPosition;
	
	private void Awake(){
		car_driver = GetComponent<Controls_Player>();
		waypoint_i = 0;
	}


    // Update is called once per frame
    void Update()
    {
		SetTargetPosition(waypoints[waypoint_i].position);
		
		Vector3 dirToMovePosition = (targetPosition - transform.position).normalized;
		float dot = Vector3.Dot(transform.forward, dirToMovePosition);
		
		Debug.Log(dot);
		
		car_driver.SetMovement(1, 0);
	}
	
	public void SetTargetPosition(Vector3 targetPosition){
		this.targetPosition = targetPosition;
	}
}