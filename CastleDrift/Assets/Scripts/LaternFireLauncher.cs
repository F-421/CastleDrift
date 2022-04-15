using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class LaternFireLauncher : MonoBehaviour
{
    const string TAG_COMPARE = "Player";  
    private float cooldown = 2f; // set time until launch another fire
    private float fire_cooldown_left; // cooldown when fire launched
    public GameObject fire; //the fire projectile to launch
    private float fire_speed = 10; // how fast is fire?
    private int num_in_field; // hwo many vehicles passing by
    
    // Start is called before the first frame update
    void Start()
    {
        fire_cooldown_left = 0;
        num_in_field = 0;
    }

    // spawn fire continuously
    void Update(){

        // only do this if we have things still in field
        if(num_in_field > 0){
            if(fire_cooldown_left <= 0){
                Debug.Log("Latern fire activate!!!");

                MakeFire();
            }

            // decrease fire cooldown counter
            else{
                fire_cooldown_left -= Time.deltaTime;
            }
        }
    }

    /*launch fire when player gets too close*/
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag(TAG_COMPARE)){
            num_in_field++;
        }
    }

    /*launch fire when player gets too close*/
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag(TAG_COMPARE)){
            num_in_field--;
        }
    }

    private void MakeFire(){
        GameObject lanternFire = (GameObject) Instantiate(fire, transform.position, fire.transform.rotation);
        lanternFire.GetComponent<Rigidbody>().AddForce(fire_speed * Vector3.down, ForceMode.Impulse);
        lanternFire.GetComponentInChildren<VisualEffect>().Play();
		//lanternFire.GetComponent<VisualEffect>().Play();
		fire_cooldown_left = cooldown;
    }
}
