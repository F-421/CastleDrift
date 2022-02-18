using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlls : MonoBehaviour
{

    //for now, we just are working with a set speed, no acceleration
    public float max_forward_speed;  // how fast can we move?
    public float rot_speed; //how fast are we turning?
    public float acceleration_max; //how fast can we accelerate?
    public float start_push_speed; //how fast we start in beginning
    public float friction_percent; //how fast we slow down (not a force. I know, confusing)

    //privately, we need to move
    private float cur_accel; //what accek do we have (based on key movement)
    private float cur_max_speed; //cur max for boosts
    private float forward_speed; //current forward speed

    public float jump_velocity; //how much force do we apply when we jump
    public int max_jump; //how many times can we jump

    private int jump_count; // how many times we have jumped (allows for double+ jump)
    private float gravity_multiplier; //makes it fall faster

    // for boosting
    private float boost_time_left; // how much time left in boost
    private bool boost_in_effect; 

    // for drifting
    public float max_velocity_drift_loss; //how much we slow down (and speed back up bc I'm lazy)
    public float drift_time; //how long we drift at max drift (and other drifts are in relation)
    private float drift_time_left; //drift time remaining
    private float drift_velocity_stored; //how much velocity are we storing?
    
    enum DriftCode {NO_DRIFT, STORE_DRIFT, RELEASE_DRIFT};
    private DriftCode isDrifting; //what stage are we 'drifting'

    // movify the physics of the rigidbody itself
    Rigidbody player_rigidBody;

    // for lap detection
    public int lapNum;
    public int checkpointNum;
    Vector3 m_LeftRotate;
    Vector3 m_RightRotate;

    void Start(){

        player_rigidBody = GetComponent<Rigidbody>();
        jump_count = 0;
        gravity_multiplier = 4.5f;

        cur_accel = 0;
        forward_speed = start_push_speed;
        cur_max_speed = max_forward_speed;

        m_LeftRotate = new Vector3(0, rot_speed, 0);
        m_RightRotate = new Vector3(0, -rot_speed, 0);

        lapNum = 1;
        checkpointNum = 0;
        boost_in_effect = false;

        isDrifting = DriftCode.NO_DRIFT;
        drift_velocity_stored = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //move forwards
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            cur_accel = acceleration_max;
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(new Vector3(0.0f, -1.0f, 0.0f) * Time.deltaTime * rot_speed, Space.World);
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f) * Time.deltaTime * rot_speed, Space.World);
            }

            //ony drift when going forward
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) && isDrifting == DriftCode.NO_DRIFT){
                isDrifting = DriftCode.STORE_DRIFT;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift)){
                Debug.Log("Let go of drift");
                ReleaseDrift();
            }
        }

        //move backwards
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            cur_accel = -acceleration_max;

            // turning the car
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(new Vector3(0.0f, -1.0f, 0.0f) * Time.deltaTime * rot_speed, Space.World);
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f) * Time.deltaTime * rot_speed, Space.World);
            }
        }

        //stop accelerating if no key pushed (decelerate)
        // apply 'friction' here (I know: it really makes no sense)
        else
        {
            cur_accel = 0;
            FakeFriction();
        }

        //store our velocity and accel
        if(isDrifting == DriftCode.STORE_DRIFT){
            if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.UpArrow)){
                ReleaseDrift();
            }
            //apply drift
            else{
                cur_accel = 0;
                if(forward_speed > max_forward_speed - max_velocity_drift_loss){
                    FakeFriction();
                }
                if(drift_velocity_stored < max_forward_speed){
                    drift_velocity_stored += max_forward_speed * friction_percent;
                }
            }
        }
        //auto drift if release forward and drifting
        

        //only increase accel if not at maximum
        if( (cur_accel > 0 && forward_speed < max_forward_speed) || 
            (cur_accel < 0 && (-1 * forward_speed) < max_forward_speed))
        {
            forward_speed += cur_accel * Time.deltaTime;
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

                forward_speed = max_forward_speed;
                cur_max_speed = max_forward_speed;
                boost_in_effect = false;
                boost_time_left = 0;
            }
        }

        // still apply drift until time is up
        if(isDrifting == DriftCode.RELEASE_DRIFT){
            drift_time_left -= Time.deltaTime;

            if(drift_time_left <= 0){
                Debug.Log("Drift Ended");

                forward_speed = max_forward_speed;
                cur_max_speed = max_forward_speed;
                cur_accel = acceleration_max;
                drift_time_left = 0;
                isDrifting = DriftCode.NO_DRIFT;
                drift_velocity_stored = 0;
            }
        }


    }

    //move depending on our speed
    void FixedUpdate()
    {
        player_rigidBody.MovePosition(transform.position + (transform.forward * forward_speed * Time.deltaTime));
    }

    //Collision handling
    void OnCollisionEnter(Collision collision)
    {

        // reset jump count if we hit ground!
        if (collision.collider.tag == "Floor")
        {
            Debug.Log("Hit ground");
            jump_count = 0;
        }

    }

    // function to handle what happens in boost
    public void TurnBoostOn(float boost_timer, float speed_boost){
        
        //don't stack speed if boost already exists
        if(!boost_in_effect){
            boost_in_effect = true;
            forward_speed += speed_boost;
            cur_max_speed += speed_boost;
        }

        boost_time_left = boost_timer;
    }

    //we drift
    public void ReleaseDrift(){
        Debug.Log("Release Drift here");
        isDrifting = DriftCode.RELEASE_DRIFT;

        //calculate new start velocity and max and speed to it
        forward_speed += drift_velocity_stored;
        cur_accel = acceleration_max * 2;

        //drift time based on how much we stored (max is drift_time)
        drift_time_left = (drift_velocity_stored / max_velocity_drift_loss) * drift_time;
    }

    // do internal 'friction' calculation
    // yes, I am faking friction in decelerating conditions at the moment
    private void FakeFriction(){
        float friction_amount = max_forward_speed * friction_percent;

        //decelerate forwards or stop
        if (forward_speed < 0) {


            if (friction_amount > forward_speed * -1) {
                forward_speed = 0;
            }
            else
            {
                forward_speed += friction_amount;
            }
        }

        //decelerate backwards or stop
        else if (forward_speed > 0) {
            if (friction_amount > forward_speed) {
                forward_speed = 0;
            }
            else{
                forward_speed -= friction_amount;
            }
        }
    }
}
