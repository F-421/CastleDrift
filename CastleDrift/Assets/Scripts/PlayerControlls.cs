using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlls : MonoBehaviour
{

    //for now, we just are working with a set speed, no acceleration
    public float forward_speed;
    public float rot_speed;

    // movify the physics of the rigidbody itself
    Rigidbody player_rigidBody;

    void Start(){

        player_rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        // move forward [simple]
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
            player_rigidBody.velocity = transform.forward * forward_speed;

            // turning the car
            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
                transform.Rotate( new Vector3(0.0f, -1.0f, 0.0f) * Time.deltaTime * rot_speed, Space.World);
            }
            else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
                transform.Rotate( new Vector3(0.0f, 1.0f, 0.0f) * Time.deltaTime * rot_speed, Space.World);
            }
        }

        // move backwards [simple]
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
            player_rigidBody.velocity = transform.forward * -forward_speed;

            // turning the car
            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
                transform.Rotate( new Vector3(0.0f, -1.0f, 0.0f) * Time.deltaTime * rot_speed, Space.World);
            }
            else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
                transform.Rotate( new Vector3(0.0f, 1.0f, 0.0f) * Time.deltaTime * rot_speed, Space.World);
            }
        }

    }
}
