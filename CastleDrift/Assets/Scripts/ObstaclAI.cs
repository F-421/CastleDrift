using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObstaclAI : MonoBehaviour
{
    const string TAG_COMPARE = "Waypoint";
    const string PLAYER_TAG_COMPARE = "Player"; // avoid retyping if I need this again
	const string AI_TAG_COMPARE = "AIKart"; // avoid retyping if I need this again

    private NavMeshAgent navAgent; // the obstacle being moved
    [SerializeField] 
    private List<Transform> waypoints; //list of places to go on obstacle route
    private int target; //what location are we at
    private GameObject cur_waypoint; //dest activating for cur waypoint


    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        cur_waypoint = null;

        if(navAgent is null){
            Debug.Log("NavMesh not acquired");
        }

        if(waypoints.Count > 1 && !(waypoints[0] is null)){
            //Debug.Log("Going to" + waypoints[0].position);
            navAgent.SetDestination(waypoints[0].position);
            target = 0;
        }
        else{
            Debug.Log("Invalid waypoints");
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        //located other waypoint
        if (other.gameObject.CompareTag(TAG_COMPARE))
        {
            // Debug.Log("Collided with waypoint");

            GameObject collidedwaypoint = other.gameObject;

            if ((cur_waypoint is not null) && (collidedwaypoint == cur_waypoint))
            {
                return;
            }

            cur_waypoint = collidedwaypoint;
            target++;

            //reached end of path: reset targets in other direction
            if (target == waypoints.Count)
            {
                waypoints.Reverse();
                target = 1;
            }

            //go to next waypoint
            if (!(waypoints[target] is null))
            {
                navAgent.SetDestination(waypoints[target].position);
                //Debug.Log("Going to" + waypoints[target].position);
            }
            else
            {
                Debug.Log("Couldn't find waypoint");
            }
        }

        /*the driver hit the snail. you monster*/
        else if (other.gameObject.CompareTag(PLAYER_TAG_COMPARE) || other.gameObject.CompareTag(AI_TAG_COMPARE))
        {
            Debug.Log("You monster. You hit the snail");
            Controls_Player player = other.gameObject.GetComponent<Controls_Player>();
            player.stagger();
        }
    }
}
