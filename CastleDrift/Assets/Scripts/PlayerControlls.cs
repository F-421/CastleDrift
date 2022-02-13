using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlls : MonoBehaviour
{

    //for now, we just are working with a set speed, no acceleration
    public float forward_speed;  // how fast are we moving?
    public float rot_speed; //how fast are we turning?

    public float jump_velocity; //how much force do we apply when we jump
    public int max_jump; //how many times can we jump

    private float ground_height; //when to detect we are on the ground
    private int jump_count; // how many times we have jumped (allows for double+ jump)
    private float gravity_multiplier; //makes it fall faster

    // movify the physics of the rigidbody itself
    Rigidbody player_rigidBody;

    void Start(){

        player_rigidBody = GetComponent<Rigidbody>();
        ground_height = player_rigidBody.position.y;
        jump_count = 0;
        gravity_multiplier = 4.5f;

        Debug.Log("ground height " + ground_height);
    }

    // Update is called once per frame
    void Update()
    {
        
        // move forward [simple]
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
            player_rigidBody.velocity = transform.forward * forward_speed;
            
            //this should apply force forward, but not sure why this isn't working
            //player_rigidBody.AddForce(transform.forward * forward_speed);

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

        // reset jump count if we hit ground
        // NOTE: This detects ground for a flat plane, we need to update to make it grounded to the current surface!!!
        if(jump_count > 0 && ground_height >= player_rigidBody.position.y){
            jump_count = 0;
        }

        //can jump in air until we can't anymore
        if(Input.GetKeyDown(KeyCode.Space) && jump_count < max_jump){

            jump_count++;

            // bonus points: each jump is weaker than the last
            //player_rigidBody.AddForce(Vector3.up * jump_force / (float)(jump_count));
            //player_rigidBody.AddForce(Vector3.up * jump_force, ForceMode.Impulse);

            player_rigidBody.velocity += Vector3.up * jump_velocity / (float)(jump_count);
            Debug.Log("Space hit, jumps:" + jump_count + " / " + max_jump);
        }

        //fall faster
        if (player_rigidBody.velocity.y < 0){
            player_rigidBody.velocity += Vector3.up * Physics2D.gravity.y * (gravity_multiplier - 1) * Time.deltaTime;
        }

        Debug.Log(player_rigidBody.velocity);


    }
}
