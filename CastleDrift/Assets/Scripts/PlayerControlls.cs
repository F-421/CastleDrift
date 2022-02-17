using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlls : MonoBehaviour
{

    //for now, we just are working with a set speed, no acceleration
    public float default_forward_speed;  // how fast are we moving?
    public float rot_speed; //how fast are we turning?

    public float jump_velocity; //how much force do we apply when we jump
    public int max_jump; //how many times can we jump

    private float ground_height; //when to detect we are on the ground
    private int jump_count; // how many times we have jumped (allows for double+ jump)
    private float gravity_multiplier; //makes it fall faster

    private float forward_speed; //current forward speed

    //for boosting
    private float boost_time_left; // how much time left in boost
    private bool boost_in_effect; 


    // movify the physics of the rigidbody itself
    Rigidbody player_rigidBody;

    // for lap detection
    public int lapNum;
    public int checkpointNum;
    Vector3 m_LeftRotate;
    Vector3 m_RightRotate;

    void Start(){

        player_rigidBody = GetComponent<Rigidbody>();
        ground_height = player_rigidBody.position.y;
        jump_count = 0;
        gravity_multiplier = 4.5f;
        forward_speed = default_forward_speed;

        m_LeftRotate = new Vector3(0, rot_speed, 0);
        m_RightRotate = new Vector3(0, -rot_speed, 0);

        Debug.Log("ground height " + ground_height);

        lapNum = 1;
        checkpointNum = 0;
        boost_in_effect = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        // move forward [simple]
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){

            // turning the car
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
                transform.Rotate( new Vector3(0.0f, -1.0f, 0.0f) * Time.deltaTime * rot_speed, Space.World);
            }
            else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){                
                transform.Rotate( new Vector3(0.0f, 1.0f, 0.0f) * Time.deltaTime * rot_speed, Space.World);

                //alternate rotation method
                //player_rigidBody.MovePosition(transform.position + (transform.right * rot_speed));
            }

            //go forward
            player_rigidBody.MovePosition(transform.position + (transform.forward * forward_speed * Time.deltaTime));
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

        //Debug.Log(player_rigidBody.velocity);

        // if boost is over, change the velocity back
        if(boost_in_effect){
            boost_time_left -= Time.deltaTime;

            if(boost_time_left <= 0){

                Debug.Log("Booster Ended");

                forward_speed = default_forward_speed;
                boost_in_effect = false;
                boost_time_left = 0;
            }
        }


    }

    //alternate method 1: me no likey
    /*
    void FixedUpdate()
    {
        float mV = Input.GetAxis("Horizontal");
        float mH = Input.GetAxis("Vertical");
        player_rigidBody.velocity = new Vector3(mH * rot_speed, player_rigidBody.velocity.y, mV * -forward_speed);
    }
    */

    // function to handle what happens in boost
    public void TurnBoostOn(float boost_timer, float speed_boost){
        
        //don't stack speed if boost already exists
        if(!boost_in_effect){
            boost_in_effect = true;
            forward_speed += speed_boost;
        }

        boost_time_left = boost_timer;
    }
}
