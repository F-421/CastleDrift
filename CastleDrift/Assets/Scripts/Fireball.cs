using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    const string TAG_PLAYER = "Player"; 
    const string TAG_GROUND = "Floor"; 
	private float fire_speed = 5;
	
	private Rigidbody fire_rb;
	void Start(){
		fire_rb = gameObject.GetComponent<Rigidbody>();
	}

    void Update()
    {
        //keep moving down
		fire_rb.AddForce(Vector3.down * fire_speed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other) {

        //stagger then delete if hit player
        if(other.gameObject.CompareTag(TAG_PLAYER)){
            Debug.Log("fire hit");

            Controls_Player player = other.gameObject.GetComponent<Controls_Player>();
            player.stagger();
            
            FireEnd();
        }

        else if(other.gameObject.CompareTag(TAG_GROUND)){
            FireEnd();
        }

        
        
    }

    // Destroy object (and any effects that need to be on destroy)
    private void FireEnd()
    {
        Destroy(gameObject);
    }
}
