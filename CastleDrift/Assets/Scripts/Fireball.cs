using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Fireball : MonoBehaviour
{
    const string PLAYER_TAG_COMPARE = "Player"; // avoid retyping if I need this again
	const string AI_TAG_COMPARE = "AIKart"; // avoid retyping if I need this again  
	
    const string TAG_GROUND = "Floor"; 
    const string TAG_GROUND2 = "FireStopper"; 
	private float fire_speed = 7;
	[SerializeField] ParticleSystem endFire = null; // fire burst when destroyed
	
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
        if(other.gameObject.CompareTag(PLAYER_TAG_COMPARE) || other.gameObject.CompareTag(AI_TAG_COMPARE)){
            Debug.Log("fire hit");

            Controls_Player player = other.gameObject.GetComponent<Controls_Player>();
            player.stagger();
            
            FireEnd();
        }

        else if(other.gameObject.CompareTag(TAG_GROUND) || other.gameObject.CompareTag(TAG_GROUND2)){
            FireEnd();
        }

        
        
    }

    // Destroy object (and any effects that need to be on destroy)
    private void FireEnd()
    {
		endFire.Play();
		StartCoroutine(delay(3));

    }

    /*can't move for x seconds*/
    IEnumerator delay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
		GetComponentInChildren<VisualEffect>().Stop();
    }
}
